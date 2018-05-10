using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using Dashing.Wellcom.Dam.Models;

namespace Dashing.Wellcom.Dam.Controllers
{
    [Route("api/{controller}")]
    public class WellcomProductController : ApiController
    {
        private readonly WellcomProductsService _wellcomProductsService;

        public WellcomProductController(WellcomProductsService wellcomProductsService)
        {
            _wellcomProductsService = wellcomProductsService;
        }

        /// <summary>
        /// Search Product by Description, Code, Barcode, Batch and BatchSize for Pagination
        /// </summary>
        /// <param name="desc"></param>
        /// <param name="code"></param>
        /// <param name="gtin"></param>
        /// <param name="batch"></param>
        /// <param name="batchSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("SearchProducts")]
        //public IHttpActionResult SearchProducts(string desc = "\"SAMBUCOL Immuno Forte Liquid 250mL\"", string code = "\"9314807043551\"", string gtin="",int batch=0,int batchSize=10)
        public IHttpActionResult SearchProducts(string desc = "", string code = "", string gtin = "", int batch = 0,
            int batchSize = 1000)
        {

            var products = _wellcomProductsService.SearchProdcucts(desc, code, gtin, batch, batchSize);
            return Ok(products.Where(p => p.HeroMedia != null).ToList());
        }

        /// <summary>
        /// This method is to Search Product by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetProduct")]
        public IHttpActionResult GetProduct([Required]string id)
        {
            var product = _wellcomProductsService.GetProduct(id);
            return Ok(product);
        }
    }
}
