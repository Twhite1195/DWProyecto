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
    public class LOTEController : ApiController
    {
        private ProyectoEntities db = new ProyectoEntities();

        // GET: api/LOTE
        public IQueryable<LOTE> GetLOTE()
        {
            return db.LOTE;
        }

        // GET: api/LOTE/5
        [ResponseType(typeof(LOTE))]
        public IHttpActionResult GetLOTE(int id)
        {
            LOTE lOTE = db.LOTE.Find(id);
            if (lOTE == null)
            {
                return NotFound();
            }

            return Ok(lOTE);
        }

        // PUT: api/LOTE/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLOTE(int id, LOTE lOTE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lOTE.LOT_ID)
            {
                return BadRequest();
            }

            db.Entry(lOTE).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LOTEExists(id))
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

        // POST: api/LOTE
        [ResponseType(typeof(LOTE))]
        public IHttpActionResult PostLOTE(LOTE lOTE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LOTE.Add(lOTE);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LOTEExists(lOTE.LOT_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = lOTE.LOT_ID }, lOTE);
        }

        // DELETE: api/LOTE/5
        [ResponseType(typeof(LOTE))]
        public IHttpActionResult DeleteLOTE(int id)
        {
            LOTE lOTE = db.LOTE.Find(id);
            if (lOTE == null)
            {
                return NotFound();
            }

            db.LOTE.Remove(lOTE);
            db.SaveChanges();

            return Ok(lOTE);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LOTEExists(int id)
        {
            return db.LOTE.Count(e => e.LOT_ID == id) > 0;
        }
    }
}