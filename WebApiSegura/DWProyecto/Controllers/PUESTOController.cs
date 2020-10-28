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
    [RoutePrefix("api/PUESTO")]
    public class PUESTOController : ApiController
    {
        private ProyectoEntities db = new ProyectoEntities();

        // GET: api/PUESTO
        public IQueryable<PUESTO> GetPUESTO()
        {
            return db.PUESTO;
        }

        // GET: api/PUESTO/5
        [ResponseType(typeof(PUESTO))]
        public IHttpActionResult GetPUESTO(int id)
        {
            PUESTO puesto = db.PUESTO.Find(id);
            if (puesto == null)
            {
                return NotFound();
            }

            return Ok(puesto);
        }

        // PUT: api/PUESTO/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPUESTO(int id, PUESTO puesto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != puesto.PUES_ID)
            {
                return BadRequest();
            }

            db.Entry(puesto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PUESTOExists(id))
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

        // POST: api/PUESTO
        [ResponseType(typeof(PUESTO))]
        public IHttpActionResult PostPUESTO(PUESTO puesto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PUESTO.Add(puesto);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = puesto.PUES_ID }, puesto);
        }

        // DELETE: api/PUESTO/5
        [ResponseType(typeof(PUESTO))]
        public IHttpActionResult DeletePUESTO(int id)
        {
            PUESTO puesto = db.PUESTO.Find(id);
            if (puesto == null)
            {
                return NotFound();
            }

            db.PUESTO.Remove(puesto);
            db.SaveChanges();

            return Ok(puesto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PUESTOExists(int id)
        {
            return db.PUESTO.Count(e => e.PUES_ID == id) > 0;
        }
    }
}