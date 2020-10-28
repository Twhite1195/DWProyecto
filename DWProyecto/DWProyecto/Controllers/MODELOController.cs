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
            MODELO mODELO = db.MODELO.Find(id);
            if (mODELO == null)
            {
                return NotFound();
            }

            return Ok(mODELO);
        }

        // PUT: api/MODELO/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMODELO(int id, MODELO mODELO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mODELO.MOD_ID)
            {
                return BadRequest();
            }

            db.Entry(mODELO).State = EntityState.Modified;

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
        public IHttpActionResult PostMODELO(MODELO mODELO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MODELO.Add(mODELO);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MODELOExists(mODELO.MOD_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = mODELO.MOD_ID }, mODELO);
        }

        // DELETE: api/MODELO/5
        [ResponseType(typeof(MODELO))]
        public IHttpActionResult DeleteMODELO(int id)
        {
            MODELO mODELO = db.MODELO.Find(id);
            if (mODELO == null)
            {
                return NotFound();
            }

            db.MODELO.Remove(mODELO);
            db.SaveChanges();

            return Ok(mODELO);
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