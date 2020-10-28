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
    [RoutePrefix("api/MARCA")]
    public class MARCAController : ApiController
    {
        private ProyectoEntities db = new ProyectoEntities();

        // GET: api/MARCA
        public IQueryable<MARCA> GetMARCA()
        {
            return db.MARCA;
        }

        // GET: api/MARCA/5
        [ResponseType(typeof(MARCA))]
        public IHttpActionResult GetMARCA(int id)
        {
            MARCA marca = db.MARCA.Find(id);
            if (marca == null)
            {
                return NotFound();
            }

            return Ok(marca);
        }

        // PUT: api/MARCA/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMARCA(int id, MARCA marca)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != marca.MAR_ID)
            {
                return BadRequest();
            }

            db.Entry(marca).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MARCAExists(id))
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

        // POST: api/MARCA
        [ResponseType(typeof(MARCA))]
        public IHttpActionResult PostMARCA(MARCA marca)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MARCA.Add(marca);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MARCAExists(marca.MAR_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = marca.MAR_ID }, marca);
        }

        // DELETE: api/MARCA/5
        [ResponseType(typeof(MARCA))]
        public IHttpActionResult DeleteMARCA(int id)
        {
            MARCA marca = db.MARCA.Find(id);
            if (marca == null)
            {
                return NotFound();
            }

            db.MARCA.Remove(marca);
            db.SaveChanges();

            return Ok(marca);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MARCAExists(int id)
        {
            return db.MARCA.Count(e => e.MAR_ID == id) > 0;
        }
    }
}