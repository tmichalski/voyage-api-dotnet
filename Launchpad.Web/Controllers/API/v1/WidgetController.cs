﻿using Launchpad.Core;
using Launchpad.Models;
using Launchpad.Services.Interfaces;
using System.Net;
using System.Web.Http;
using static Launchpad.Web.Constants;
using Launchpad.Web.Filters;

namespace Launchpad.Web.Controllers.API.V1
{


    [Authorize]
    [RoutePrefix(Constants.RoutePrefixes.V1)]
    public class WidgetController : ApiController
    {
        private IWidgetService _widgetService;

        public WidgetController(IWidgetService widgetService)
        {
            _widgetService = widgetService.ThrowIfNull(nameof(widgetService));
        }

        /**
         * @api {get} /v1/widget Get all widgets
         * @apiVersion 0.1.0
         * @apiName GetWidgets
         * @apiGroup Widget
         * 
         * @apiPermission lss.permission->list.widgets
         * 
         * @apiSuccess {Object[]} widgets List of widgets.
         * @apiSuccess {String} widgets.name Name of the widget.
         * @apiSuccess {Number} widgets.id ID of the widget.
         *
         * @apiSuccessExample Success-Response:
         *      HTTP/1.1 200 OK
         *      [
         *       {
         *          "id": 3,
         *          "name": "Large Widget"
         *       },
         *       {
         *          "id": 7,
         *          "name": "Medium Widget"
         *       }
         *      ]
         * 
         * @apiUse AuthHeader
         * @apiUse UnauthorizedError
         */
        [ClaimAuthorize(ClaimValue =LssClaims.ListWidgets)]
        [Route("widget")]
        public IHttpActionResult Get()
        {
            return Ok(_widgetService.GetWidgets());
        }


        /**
         * @api {get} /v1/widget/:id Get a widget
         * @apiVersion 0.1.0
         * @apiName GetWidget
         * @apiGroup Widget
         *
         * @apiPermission lss.permission->view.widget
         * 
         * @apiParam {Number} id widget's unique ID.
         *
         * @apiSuccess {String} name Name of the widget.
         * @apiSuccess {Number} id ID of the widget.
         *
         * @apiSuccessExample Success-Response:
         *     HTTP/1.1 200 OK
         *     {
         *        "id": 3,
         *        "name": "Large Widget"
         *     }
         *
         * @apiUse NotFoundError
         * @apiUse AuthHeader
         * @apiUse UnauthorizedError
         */
        [ClaimAuthorize(ClaimValue = LssClaims.ViewWidget)]
        [Route("widget/{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var widget = _widgetService.GetWidget(id);
            if (widget == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(widget);
            }
        }

        /**
         * @api {post} /v1/widget Create a new widget
         * @apiVersion 0.1.0
         * @apiName CreateWidget
         * @apiGroup Widget
         * 
         * @apiPermission lss.permission->create.widget
         * 
         * @apiParam {String} name Name of the widget
         * 
         * @apiSuccess {Object} widget New widget
         * @apiSuccess {String} widget.name Name of the widget
         * @apiSuccess {Number} widget.id ID of the widget
         * 
         * @apiSuccessExample Success-Response:
         *      HTTP/1.1 201 CREATED      
         *      {
         *           "id": 70,
         *           "name": "Small Widget"
         *      }
         *      
         * @apiUse BadRequestError
         * @apiUse AuthHeader
         * @apiUse UnauthorizedError
         */
        [ClaimAuthorize(ClaimValue = LssClaims.CreateWidget)]
        [Route("widget")]
        [HttpPost]
        public IHttpActionResult AddWidget([FromBody] WidgetModel widget)
        {
            WidgetModel model = _widgetService.AddWidget(widget);
            
            return Created($"/api/widget/{model.Id}", model);
        }

        /**
         * @api {delete} /v1/widget/:id Delete a widget
         * @apiVersion 0.1.0 
         * @apiName DeleteWidget
         * @apiGroup Widget
         * 
         * @apiPermission lss.permission->delete.widget
         * 
         * @apiParam {Number} id ID of the widget which will be deleted
         * 
         * @apiSuccessExample Success-Response:
         *      HTTP/1.1 204
         *      
         * @apiUse AuthHeader
         * @apiUse UnauthorizedError
         * 
         */
        [ClaimAuthorize(ClaimValue = LssClaims.DeleteWidget)]
        [Route("widget/{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteWidget(int id)
        {
            _widgetService.DeleteWidget(id);
            return StatusCode(HttpStatusCode.NoContent);
        }

        /**
         * @api {put} /v1/widget Update an existing widget
         * @apiVersion 0.1.0
         * @apiName UpdateWidget
         * @apiGroup Widget
         * 
         * @apiPermission lss.permission->update.widget
         * 
         * @apiParam {String} name Name of the widget
         * @apiParam {Number} id ID of the widget
         * 
         * @apiSuccess {Object} widget Updated widget
         * @apiSuccess {String} widget.name Name of the widget
         * @apiSuccess {Number} widget.id ID of the widget
         * 
         * @apiUse NotFoundError
         * @apiUse BadRequestError   
         * @apiUse AuthHeader
         * @apiUse UnauthorizedError
         */
        [ClaimAuthorize(ClaimValue = LssClaims.UpdateWidget)]
        [Route("widget")]
        [HttpPut]
        public IHttpActionResult UpdateWidget([FromBody] WidgetModel widget)
        {
            WidgetModel model = _widgetService.UpdateWidget(widget);

            if (model == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(model);
            }
        }

     
    }
}
