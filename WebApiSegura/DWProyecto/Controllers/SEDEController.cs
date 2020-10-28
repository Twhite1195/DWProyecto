using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DWProyecto.Models;

namespace DWProyecto.Controllers
{
    [Authorize]
    [RoutePrefix("api/SEDE")]
    public class SEDEController : ApiController
    {
        private ProyectoEntities db = new ProyectoEntities();

        // GET: api/SEDE
        public IQueryable<SEDE> GetSEDE()
        {
            return db.SEDE;
        }

        // GET: api/SEDE/5
        [ResponseType(typeof(SEDE))]
        public IHttpActionResult GetSEDE(int id)
        {
            SEDE sede = db.SEDE.Find(id);
            if (sede == null)
            {
                return NotFound();
            }

            return Ok(sede);
        }

        // PUT: api/SEDE/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSEDE(int id, SEDE sede)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sede.SED_ID)
            {
                return BadRequest();
            }

            db.Entry(sede).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SEDEExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/SEDE
        [ResponseType(typeof(SEDE))]
        public IHttpActionResult PostSEDE(SEDE sede)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SEDE.Add(sede);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SEDEExists(sede.SED_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = sede.SED_ID }, sede);
        }

        // DELETE: api/SEDE/5
        [ResponseType(typeof(SEDE))]
        public IHttpActionResult DeleteSEDE(int id)
        {
            SEDE sede = db.SEDE.Find(id);
            if (sede == null)
            {
                return NotFound();
            }

            db.SEDE.Remove(sede);
            db.SaveChanges();

            return Ok(sede);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SEDEExists(int id)
        {
            return db.SEDE.Count(e => e.SED_ID == id) > 0;
        }
    }
}