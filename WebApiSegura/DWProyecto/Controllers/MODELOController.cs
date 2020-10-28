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
    [RoutePrefix("api/MODEL")]
    public class MODELOController : ApiController
    {
        private ProyectoEntities db = new ProyectoEntities();

        // GET: api/MODELO
        public IQueryable<MODELO> GetMODELO()
        {
            return db.MODELO;
        }

        // GET: api/MODELO/5
        [ResponseType(typeof(MODELO))]
        public IHttpActionResult GetMODELO(int id)
        {
            MODELO modelo = db.MODELO.Find(id);
            if (modelo == null)
            {
                return NotFound();
            }

            return Ok(modelo);
        }

        // PUT: api/MODELO/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMODELO(int id, MODELO modelo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != modelo.MOD_ID)
            {
                return BadRequest();
            }

            db.Entry(modelo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MODELOExists(id))
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

        // POST: api/MODELO
        [ResponseType(typeof(MODELO))]
        public IHttpActionResult PostMODELO(MODELO modelo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MODELO.Add(modelo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MODELOExists(modelo.MOD_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = modelo.MOD_ID }, modelo);
        }

        // DELETE: api/MODELO/5
        [ResponseType(typeof(MODELO))]
        public IHttpActionResult DeleteMODELO(int id)
        {
            MODELO modelo = db.MODELO.Find(id);
            if (modelo == null)
            {
                return NotFound();
            }

            db.MODELO.Remove(modelo);
            db.SaveChanges();

            return Ok(modelo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MODELOExists(int id)
        {
            return db.MODELO.Count(e => e.MOD_ID == id) > 0;
        }
    }
}