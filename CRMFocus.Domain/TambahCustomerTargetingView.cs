using CRMFocus.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRMFocus.Domain
{
    public class TambahCustomerTargetingView
    {       
        [StringLength(50)]
        public string TargetCustumerName { get; set; }

        // Lead properties

        [StringLength(2)]
        public string Profesion { get; set; } // Pekerjaan

        [StringLength(1)]
        public string Gender { get; set; } // Jenis Kelamin

        [StringLength(1)]
        public string Religion { get; set; } // Agama

        public DateTime BirthDate1 { get; set; } // Tanggal Lahir1

        public int Year1 { get; set; } // Year1

        public DateTime BirthDate2 { get; set; } // Tanggal Lahir

        public int Year2 { get; set; } // Year2

        [StringLength(2)]
        public string Spending { get; set; } // Spending


        // Lead Unit properties

        public string JumlahRO { get; set; }

        [StringLength(50)]
        public string ProvinceCode { get; set; } // Provinsi

        [StringLength(50)]
        public string KabupatenCode { get; set; } // Kabupaten

        [StringLength(50)]
        public string KecamatanCode { get; set; } // Kecamatan

        [StringLength(50)]
        public string KelurahanCode { get; set; } // Kelurahan

        [StringLength(20)]
        public string UnitTypeSegment { get; set; } // Market Segmen

        [StringLength(50)]
        public string UnitTypeSeries { get; set; } // Market Type

        [StringLength(50)]
        public string UnitMarketName { get; set; } // Market Name

        public byte ResourceType { get; set; } // Customer dari
 
        public byte DestinationType { get; set; } // Tujuan

        public DateTime? StartDate { get; set; } // Start date

        public DateTime? EndDate { get; set; } // End date

        public List<FilterLainView> FilterLainView { get; set; } // Filter lain
    }
}
