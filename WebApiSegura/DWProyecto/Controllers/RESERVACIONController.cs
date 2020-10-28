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
            RESERVACION rESERVACION = db.RESERVACION.Find(id);
            if (rESERVACION == null)
            {
                return NotFound();
            }

            return Ok(rESERVACION);
        }

        // PUT: api/RESERVACION/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRESERVACION(int id, RESERVACION rESERVACION)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rESERVACION.RES_ID)
            {
                return BadRequest();
            }

            db.Entry(rESERVACION).State = EntityState.Modified;

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
        public IHttpActionResult PostRESERVACION(RESERVACION rESERVACION)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RESERVACION.Add(rESERVACION);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (RESERVACIONExists(rESERVACION.RES_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = rESERVACION.RES_ID }, rESERVACION);
        }

        // DELETE: api/RESERVACION/5
        [ResponseType(typeof(RESERVACION))]
        public IHttpActionResult DeleteRESERVACION(int id)
        {
            RESERVACION rESERVACION = db.RESERVACION.Find(id);
            if (rESERVACION == null)
            {
                return NotFound();
            }

            db.RESERVACION.Remove(rESERVACION);
            db.SaveChanges();

            return Ok(rESERVACION);
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