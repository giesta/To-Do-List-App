﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Services.ToDoList
{
    public interface IHasId
    {
        public int Id { get; set; }
    }
}