﻿using Launchpad.Models;
using Launchpad.Services.Interfaces;
using System.Collections.Generic;
using System.Web.Http;

namespace Launchpad.Web.Controllers.API
{
    public class WidgetController : ApiController
    {
        private IWidgetService _widgetService;

        public WidgetController(IWidgetService widgetService)
        {
            _widgetService = widgetService;
        }

        public IEnumerable<WidgetModel> Get()
        {
            return _widgetService.GetWidgets();
        }
    }
}
