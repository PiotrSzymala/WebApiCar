﻿using WebApiCar.Infrastructure.DatabseContexts;
using WebApiCar.Infrastructure.Repositories;

namespace WebApiCar.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }


    }
}
