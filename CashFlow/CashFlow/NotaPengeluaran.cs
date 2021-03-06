﻿using dokuku.Dto;
using dokuku.exceptions;
using dokuku.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku
{
    public class NotaPengeluaran
    {
        private DateTime _dateTime;
        private string _noNota;       
        private double _totalNota;
        List<AkunPengeluaran> _itemsAkun = new List<AkunPengeluaran>();         

        public NotaPengeluaran(DateTime dateTime, string noNota)
        {      
            this._dateTime = dateTime;
            this._noNota = noNota;
            CalculateNotaPengeluaran();
        }

       
        public Dto.NotaPengeluaranDto Snap()
        {
            return new Dto.NotaPengeluaranDto()
            {
                Tanggal = this._dateTime,
                NoNota = this._noNota,                
                TotalNota = this._totalNota,
                Items = SetToItems()
            };
        }

        private List<dokuku.Dto.NotaPengeluaranDto.ItemNotaDto>  SetToItems()
        {
            return this._itemsAkun.Select(x => x.SnapAkun()).ToList();                      
        }

        public void AddAkun(string akun, double nominal, int jumlah)
        {
            var newAkun = new AkunPengeluaran(akun, nominal, jumlah);
            bool checkAkun = _itemsAkun.Where(x => x.Akun == akun).Count() == 0 ? true : false;
            if (checkAkun)
            {
                this._itemsAkun.Add(newAkun);
            }
            else
            {
                throw new AcountAllreadyExistException();
            }

            CalculateNotaPengeluaran();
        }
        private class AkunPengeluaran
        {
            private string _akun;
            private double _nominal;
            private int _jumlah;

            public AkunPengeluaran(string akun, double nominal, int jumlah)
            {
                this._akun = akun;
                this._nominal = nominal;
                this._jumlah = jumlah;
            }
            public dokuku.Dto.NotaPengeluaranDto.ItemNotaDto SnapAkun()
            {
                return new dokuku.Dto.NotaPengeluaranDto.ItemNotaDto()
                {
                    Akun = this._akun,
                    Jumlah = this._jumlah,
                    Nominal = this._nominal
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

            public int Jumlah
            {
                get
                {
                    return this._jumlah;
                }
            }
            public void Change(double nominal)
            {
                this._nominal = nominal;
            }
            public void ChangeJumlah(int jumlah)
            {
                this._jumlah = jumlah;
            }            
        }

        public void ChangeNotaPengeluaran(string akun, double nominal, int jumlah)
        {
            var notaPengeluaran = this._itemsAkun.FirstOrDefault(x => x.Akun == akun);            
            if (notaPengeluaran == null)
            {
                this._itemsAkun.Add(new AkunPengeluaran(akun, nominal, jumlah));
            }
            else
            {
                notaPengeluaran.Change(nominal);
                notaPengeluaran.ChangeJumlah(jumlah);
            }
            CalculateNotaPengeluaran();  
        }
       
        private void CalculateNotaPengeluaran() 
        {
            this._totalNota = this.CalculateNominalPengeluaran();
        }

        private double CalculateNominalPengeluaran()
        {
            return this._itemsAkun.Sum(x => x.Nominal);
        }


       
    }
}
