﻿using ToniAuto2003.Core.Models.Home;

namespace ToniAuto2003.Core.Contracts.Car
{
    public interface ICarService
    {
        Task<IEnumerable<CarIndexServiceModel>> LastThreeCars();
    }
}
