using Microsoft.AspNet.OData;
using OdataExample.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace OdataExample.Controllers.WebapiController
{
    public class BatchesController : ODataController
    {
        BatchManagementEntities ctx = new BatchManagementEntities();
        [EnableQuery]
        [System.Web.Http.HttpGet]
        public IQueryable<BatchManagement> Get()
        {
            return ctx.BatchManagements.AsQueryable();
        }

        public IHttpActionResult Post(BatchManagement batch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ctx.BatchManagements.Add(batch);
            ctx.SaveChanges();
            return Created(batch);
        }

        public IHttpActionResult Put([FromODataUri] int key, Delta<BatchManagement> Put)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BatchManagement batch = ctx.BatchManagements.Find(key);
            if (batch == null)
            {
                return NotFound();
            }
            Put.Put(batch);
            try
            {
                ctx.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BatchExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(batch);
        }

        public IHttpActionResult Patch([FromODataUri] int key, Delta<BatchManagement> Patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BatchManagement batch = ctx.BatchManagements.Find(key);
            if (batch == null)
            {
                return NotFound();
            }
            Patch.Patch(batch);
            try
            {
                ctx.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BatchExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(batch);
        }

        public bool BatchExists(int key)
        {
            if (ctx.BatchManagements.Where(p => p.BatchId == key).Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IHttpActionResult Delete([FromODataUri] int key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BatchManagement batch = ctx.BatchManagements.Find(key);
            if (batch == null)
            {
                return NotFound();
            }
            ctx.BatchManagements.Remove(batch);
            ctx.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
 }
