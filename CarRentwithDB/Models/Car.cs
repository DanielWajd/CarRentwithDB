﻿using CarRentwithDB.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentwithDB.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; } 

        [Required]
        [StringLength(50)]
        public string Make { get; set; } 

        [Required]
        [StringLength(50)]
        public string Model { get; set; } 

        [Required]
        public int Year { get; set; }
        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        public CarType carType { get; set; }

        [Required]
        public FuelType fuelType { get; set; }

        [Required]
        public int Mileage { get; set; }

        [Required]
        public CarColor carColor { get; set; }

        [Required]
        public decimal DailyRate { get; set; }

        [Required]
        [StringLength(20)]
        public string VIN { get; set; }
        [Required]
        public SteeringSide steeringSide { get; set; }

        [Required]
        [StringLength(10)]
        public string LicencePlate { get; set; }

        public bool IsAvailable { get; set; }
        public bool IsActive { get; set; } = true;
        [Required]
        public string Description { get; set; }
        //Dawniej zdjecie z URL
        //public string Image { get; set; }
        //Do zdjecia z pliku
        [Required]
        public byte[] ImageData { get; set; }
        [NotMapped]
        public string Image { get; set; }
        ///
        public CarDetails CarDetails { get; set; }

        //musi byc nullable - inaczej blad kaskadowosci. Walidacja w kontrolerach
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
