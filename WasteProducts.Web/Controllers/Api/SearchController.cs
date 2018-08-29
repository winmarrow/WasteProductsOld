﻿using NLog;
using Swagger.Net.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Lucene.Net.Analysis.Hunspell;
using WasteProducts.Logic.Common.Models;
using WasteProducts.Logic.Common.Models.Products;
using WasteProducts.Logic.Common.Models.Search;
using WasteProducts.Logic.Common.Services;

namespace WasteProducts.Web.Controllers.Api
{
    [RoutePrefix("api/search")]
    public class SearchController : BaseApiController
    {
        private ISearchService _searchService { get; }
        public const string DEFAULT_PRODUCT_NAME_FIELD = "Name";
        public const string DEFAULT_PRODUCT_DESCRIPTION_FIELD = "Description";

        public SearchController(ILogger logger, ISearchService searchService) : base(logger)
        {
            _searchService = searchService;
        }


        [HttpGet]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "Get search result collection", typeof(IEnumerable<Product>))]
        [SwaggerResponse(HttpStatusCode.NoContent, "Search result collection is empty", typeof(IEnumerable<Product>))]
        [Route("products/default")]
        public IEnumerable<Product> GetProductsDefaultFields([FromUri]string query)
        {
            SearchQuery searchQuery = new SearchQuery();
            searchQuery.AddField(DEFAULT_PRODUCT_NAME_FIELD).AddField(DEFAULT_PRODUCT_DESCRIPTION_FIELD);
            searchQuery.Query = query;

            return _searchService.Search<Product>(searchQuery);
        }

        [HttpGet]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "Get search result collection", typeof(IEnumerable<Product>))]
        [SwaggerResponse(HttpStatusCode.NoContent, "Search result collection is empty", typeof(IEnumerable<Product>))]
        [Route("products/custom")]
        public IEnumerable<Product> GetProducts([FromUri]string query, [FromUri]string[] fields)
        {
            IEnumerable<Product> searchResultList = new List<Product>();

            SearchQuery searchQuery = new SearchQuery();
            searchQuery.Query = query;
            foreach (string filed in fields)
            {
                searchQuery.AddField(filed);
            }

            return _searchService.Search<Product>(searchQuery);
        }

        [HttpGet]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "Get search result collection", typeof(IEnumerable<Product>))]
        [SwaggerResponse(HttpStatusCode.NoContent, "Search result collection is empty", typeof(IEnumerable<Product>))]
        [Route("products/boosted")]
        public IEnumerable<Product> GetProductsBoostedFields(BoostedSearchQuery query)
        {

            //Add product for testing purposes
            Product product = new Product();
            product.Name = "Test product name";
            product.Description = "Test product description";
            _searchService.AddToSearchIndex<Product>(product);

            return _searchService.Search<Product>(query);
        }

        [HttpGet]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "Get search result collection", typeof(IEnumerable<Product>))]
        [SwaggerResponse(HttpStatusCode.NoContent, "Search result collection is empty", typeof(IEnumerable<Product>))]
        [Route("products/custom/fields")]
        public IEnumerable<Product> GetProducts(SearchQuery query)
        {
            //Add product for testing purposes
            Product product = new Product();
            product.Name = "Test product name";
            product.Description = "Test product description";
            _searchService.AddToSearchIndex<Product>(product);

            return _searchService.Search<Product>(query);
        }
    }
}
