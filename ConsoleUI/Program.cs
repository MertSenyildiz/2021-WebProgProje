using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            IGameService _gameService = new GameManager(new EfGameDal());
            var result=_gameService.Add(new Game(){GameName="TestTestGame"});
            Console.WriteLine(result.Success);
        }
    }
}
