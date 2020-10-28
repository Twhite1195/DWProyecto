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
    [RoutePrefix("api/RESERVACION")]
    public class RESERVACIONController : ApiController
    {
        private ProyectoEntities db = new ProyectoEntities();

        // GET: api/RESERVACION
        public IQueryable<RESERVACION> GetRESERVACION()
        {
            return db.RESERVACION;
        }

        // GET: api/RESERVACION/5
        [ResponseType(typeof(RESERVACION))]
        public IHttpActionResult GetRESERVACION(int id)
        {
            RESERVACION reservacion = db.RESERVACION.Find(id);
            if (reservacion == null)
            {
                return NotFound();
            }

            return Ok(reservacion);
        }

        // PUT: api/RESERVACION/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRESERVACION(int id, RESERVACION reservacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reservacion.RES_ID)
            {
                return BadRequest();
            }

            db.Entry(reservacion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RESERVACIONExists(id))
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

        // POST: api/RESERVACION
        [ResponseType(typeof(RESERVACION))]
        public IHttpActionResult PostRESERVACION(RESERVACION reservacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RESERVACION.Add(reservacion);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (RESERVACIONExists(reservacion.RES_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = reservacion.RES_ID }, reservacion);
        }

        // DELETE: api/RESERVACION/5
        [ResponseType(typeof(RESERVACION))]
        public IHttpActionResult DeleteRESERVACION(int id)
        {
            RESERVACION reservacion = db.RESERVACION.Find(id);
            if (reservacion == null)
            {
                return NotFound();
            }

            db.RESERVACION.Remove(reservacion);
            db.SaveChanges();

            return Ok(reservacion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RESERVACIONExists(int id)
        {
            return db.RESERVACION.Count(e => e.RES_ID == id) > 0;
        }
    }
}