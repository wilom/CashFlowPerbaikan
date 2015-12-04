using dokuku.Dto;
using dokuku.exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dokuku;
using dokuku.interfaces;

namespace dokuku.CashFlowHead
{
    public class CashFlow : ICashFlow
    {        
        private string _tenanId;
        private PeriodeId _periodId;
        private double _saldoAwal;
        private double _saldoAkhir;
        private double _totalPenjualan;
        private double _totalPenjualanLain;
        private double _totalPengeluaran;
        List<Penjualan> _itemsPenjualan = new List<Penjualan>();
        List<PenjualanLain> _itemsPenjualanLain = new List<PenjualanLain>();
        List<Pengeluaran> _itemsPengeluaran = new List<Pengeluaran>();
        
        
        public CashFlow(string tenanId, PeriodeId periodId, double saldoAwal) 
        {
            this._tenanId = tenanId;
            this._periodId = periodId;
            this._saldoAwal = saldoAwal;
            Calculate();                        
        }

        public CashFlow(CashFlowDto snapshot)
        {        
           
           this._tenanId=snapshot.TenantId;
           this._periodId = new PeriodeId(snapshot.PeriodId.StartPeriode, snapshot.PeriodId.EndPeriode);
           this._saldoAwal = snapshot.SaldoAwal;
           this._saldoAkhir = snapshot.SaldoAkhir;
           this._totalPenjualan = snapshot.TotalPenjualan;
           this._totalPenjualanLain = snapshot.TotalPenjualanLain;
           this._totalPengeluaran = snapshot.TotalPengeluaran;
           this._itemsPenjualan = ConvertToItemsPenjualan(snapshot.ItemsPenjualan);
           this._itemsPenjualanLain = ConvertToItemsPenjualanLain(snapshot.ItemsPenjualanLain);
           this._itemsPengeluaran = ConvertToItemsPengeluaran(snapshot.ItemsPengeluaran);
            
        }
        //konvert method
        
        public List<Penjualan> ConvertToItemsPenjualan(List<CashFlowDto.ItemsPenjualanDto> itemsPenjualanDto)
        {
            return itemsPenjualanDto.Select(x => new Penjualan(x.DateTime, x.Nominal)).ToList();
        }
        public List<PenjualanLain> ConvertToItemsPenjualanLain(List<CashFlowDto.ItemsPenjualanLainDto> itemsPenjualanLainDto)
        {
            return itemsPenjualanLainDto.Select(y => new PenjualanLain(y.DateTimeLain, y.NominalLain)).ToList();
        }
        public List<Pengeluaran> ConvertToItemsPengeluaran(List<CashFlowDto.ItemsPengeluaranDto> itemsPengeluaranDto)
        {
            return itemsPengeluaranDto.Select(z => new Pengeluaran(z.Akun, z.Nominal, z.Jumlah)).ToList();
        }
        //end konvert method
       
        public Dto.CashFlowDto Snap()
        {
            return new Dto.CashFlowDto()
            {
                TenantId = this._tenanId,
                PeriodId = this._periodId.Snap(),
                SaldoAwal = this._saldoAwal,
                SaldoAkhir = this._saldoAkhir,
                TotalPenjualan = this._totalPenjualan,
                TotalPenjualanLain = this._totalPenjualanLain,
                TotalPengeluaran = this._totalPengeluaran,
                ItemsPenjualan = SetToItemsPenjualan(),
                ItemsPenjualanLain = SetToItemsPenjualanLain(),
                ItemsPengeluaran = SetToItemsPengeluaran()
            };
        }
        private List<dokuku.Dto.CashFlowDto.ItemsPenjualanDto> SetToItemsPenjualan()
        {
            return this._itemsPenjualan.Select(x => x.SnapPenjualan()).ToList();
        }
        private List<dokuku.Dto.CashFlowDto.ItemsPenjualanLainDto> SetToItemsPenjualanLain()
        {
            return this._itemsPenjualanLain.Select(x => x.SnapPenjualanLain()).ToList();
        }
        private List<dokuku.Dto.CashFlowDto.ItemsPengeluaranDto> SetToItemsPengeluaran()
        {
            return this._itemsPengeluaran.Select(x => x.SnapPengeluaran()).ToList();
        }
        public class Penjualan
        {
            private DateTime _dateTime;
            private double _nominal;
            
            public Penjualan(DateTime date, double nominal)
            {
                this._dateTime = date;
                this._nominal = nominal;
            }
            public dokuku.Dto.CashFlowDto.ItemsPenjualanDto SnapPenjualan()
            {
                return new dokuku.Dto.CashFlowDto.ItemsPenjualanDto()
                {
                    DateTime = this._dateTime,                    
                    Nominal = this._nominal
                };
            }
            public double Nominal
            {
                get
                {
                    return this._nominal;
                }
            }
            public DateTime Tanggal
            {
                get
                {
                    return this._dateTime;
                }
            }          
        }

        public void AddPenjualan(DateTime date, double nominal)
        {
            var newSales = new Penjualan(date, nominal);
            bool checktgl = _itemsPenjualan.Where(x => x.Tanggal == date).Count() == 0 ? true : false;
            if (checktgl)
            {
                this._itemsPenjualan.Add(newSales);
            }
            else
            {
                throw new DateAlreadyExistException();
            }
            Calculate();
        }      

        private double CalculatePenjualan()
        {
            return this._itemsPenjualan.Sum(x => x.Nominal);
        }

        //mulain saleslain
        public class PenjualanLain
        {
            private DateTime _dateTimeLain;
            private double _nominalLain;
            public PenjualanLain(DateTime dateLain, double nominalLain)
            {
                this._dateTimeLain = dateLain;
                this._nominalLain = nominalLain;
            }
            public dokuku.Dto.CashFlowDto.ItemsPenjualanLainDto SnapPenjualanLain()
            {
                return new dokuku.Dto.CashFlowDto.ItemsPenjualanLainDto()
                {
                    DateTimeLain = this._dateTimeLain,
                    NominalLain = this._nominalLain
                };
            }
            public double NominalLain
            {
                get
                {
                    return this._nominalLain;
                }
            }
            public DateTime TanggalLain
            {
                get
                {
                    return this._dateTimeLain;
                }
            }
            
        }

        public void AddPenjualanLain(DateTime dateLain, double nominalLain)
        {
            var newSalesLain = new PenjualanLain(dateLain, nominalLain);
            bool checktgl = _itemsPenjualanLain.Where(x => x.TanggalLain == dateLain).Count() == 0 ? true : false;

            if (checktgl)
                this._itemsPenjualanLain.Add(newSalesLain);
            else
                throw new DateAlreadyExistException();
            Calculate();
        }

        private double CalculatePenjualanLain()
        {
            return this._itemsPenjualanLain.Sum(x => x.NominalLain);
        }

        //muali pengeluaran-----------
        public class Pengeluaran 
        {
            private string _akun;
            private double _nominal;
            private int _jumlah;

            public Pengeluaran(string akun, double nominal, int jumlah)
            {
                this._akun = akun;
                this._nominal = nominal;
                this._jumlah = jumlah;
            }
            public dokuku.Dto.CashFlowDto.ItemsPengeluaranDto SnapPengeluaran()
            {
                return new dokuku.Dto.CashFlowDto.ItemsPengeluaranDto()
                {
                    Akun = this._akun,
                    Nominal = this._nominal,
                    Jumlah = this._jumlah
                };
            }
            public string Akun 
            {
                get
                {
                return this._akun;
                }
            }
            public double Nominal
            {
                get
                {
                    return this._nominal;
                }
            }
            public int Jumalah
            {
                get
                {
                    return this._jumlah;
                }
            }
            public void ChangeNominal(double nominal) 
            {
                this._nominal = nominal;
            }
            public void ChangeJumlah(int jumlah)
            {
                this._jumlah = jumlah;
            }
        }

        public void ChangePengeluaran(string akun, double nominal, int jumlah) 
        {
            var pengeluaran = this._itemsPengeluaran.FirstOrDefault(x => x.Akun == akun);

            if (pengeluaran == null)
            {
                this._itemsPengeluaran.Add(new Pengeluaran(akun,nominal, jumlah));
            }
            else
            {
                pengeluaran.ChangeNominal(nominal);
                pengeluaran.ChangeJumlah(jumlah);
            }            
            Calculate();  
        }
       
        private double CalculatePengeluaran()
        {
            return this._itemsPengeluaran.Sum(x => x.Nominal);
        }

        private void Calculate()
        {
            this._totalPenjualan = CalculatePenjualan();

            this._totalPenjualanLain = CalculatePenjualanLain();

            this._totalPengeluaran = CalculatePengeluaran();

            this._saldoAkhir = this._saldoAwal + this._totalPenjualan + this._totalPenjualanLain - this._totalPengeluaran;
        }


        public CashFlowId GenerateId()
        {
            return new CashFlowId(this._periodId);
        }
    }    
}
