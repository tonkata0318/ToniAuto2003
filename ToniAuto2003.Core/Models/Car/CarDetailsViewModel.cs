﻿using ToniAuto2003.Core.Contracts;

namespace ToniAuto2003.Core.Models.Car
{
    public class CarDetailsViewModel : ICarModel
    {
        public int Id { get; set; }

        public int Year { get; set; }

        public string Make { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;
    }
}
