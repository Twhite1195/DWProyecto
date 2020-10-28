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
            PUESTO pUESTO = db.PUESTO.Find(id);
            if (pUESTO == null)
            {
                return NotFound();
            }

            return Ok(pUESTO);
        }

        // PUT: api/PUESTO/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPUESTO(int id, PUESTO pUESTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pUESTO.PUES_ID)
            {
                return BadRequest();
            }

            db.Entry(pUESTO).State = EntityState.Modified;

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
        public IHttpActionResult PostPUESTO(PUESTO pUESTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PUESTO.Add(pUESTO);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pUESTO.PUES_ID }, pUESTO);
        }

        // DELETE: api/PUESTO/5
        [ResponseType(typeof(PUESTO))]
        public IHttpActionResult DeletePUESTO(int id)
        {
            PUESTO pUESTO = db.PUESTO.Find(id);
            if (pUESTO == null)
            {
                return NotFound();
            }

            db.PUESTO.Remove(pUESTO);
            db.SaveChanges();

            return Ok(pUESTO);
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