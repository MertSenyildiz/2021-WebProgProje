using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            IGameService _gameService = new GameManager(new EfGameDal());
            foreach (var item in _gameService.GetAll().Data)
                Console.WriteLine(item.GameName);
        }
    }
}
