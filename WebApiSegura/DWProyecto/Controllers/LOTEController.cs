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
    [RoutePrefix("api/LOTE")]
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
            LOTE lote = db.LOTE.Find(id);
            if (lote == null)
            {
                return NotFound();
            }

            return Ok(lote);
        }

        // PUT: api/LOTE/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLOTE(int id, LOTE lote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lote.LOT_ID)
            {
                return BadRequest();
            }

            db.Entry(lote).State = EntityState.Modified;

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
        public IHttpActionResult PostLOTE(LOTE lote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LOTE.Add(lote);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LOTEExists(lote.LOT_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = lote.LOT_ID }, lote);
        }

        // DELETE: api/LOTE/5
        [ResponseType(typeof(LOTE))]
        public IHttpActionResult DeleteLOTE(int id)
        {
            LOTE lote = db.LOTE.Find(id);
            if (lote == null)
            {
                return NotFound();
            }

            db.LOTE.Remove(lote);
            db.SaveChanges();

            return Ok(lote);
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