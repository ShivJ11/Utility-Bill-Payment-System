﻿using DAL.Models.Cards;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuisnessServices.CardsAPI.Contract
{
    public interface IAddCard
    {
        Task<CardsOutput> AddCards(CardDetails input);
    }
}
