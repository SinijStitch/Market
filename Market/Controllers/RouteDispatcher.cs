using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Market.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Lib;

namespace Market.Models
{
    public static class RouteDispatcher 
    {
        public static string PreviousPage;
        public static int Id;

        //public static string GoPrevious()
        //{
        //    return RedirectToAction(PreviousPage); ;
        //}
    }
}
