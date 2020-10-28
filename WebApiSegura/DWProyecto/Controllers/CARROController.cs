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
    [RoutePrefix("api/CARRO")]
    public class CARROController : ApiController
    {
        private ProyectoEntities db = new ProyectoEntities();

        // GET: api/CARRO
        public IQueryable<CARRO> GetCARRO()
        {
            return db.CARRO;
        }

        // GET: api/CARRO/5
        [ResponseType(typeof(CARRO))]
        public IHttpActionResult GetCARRO(int id)
        {
            CARRO carro = db.CARRO.Find(id);
            if (carro == null)
            {
                return NotFound();
            }

            return Ok(carro);
        }

        // PUT: api/CARRO/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCARRO(int id, CARRO carro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != carro.CAR_ID)
            {
                return BadRequest();
            }

            db.Entry(carro).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CARROExists(id))
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

        // POST: api/CARRO
        [ResponseType(typeof(CARRO))]
        public IHttpActionResult PostCARRO(CARRO carro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CARRO.Add(carro);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CARROExists(carro.CAR_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = carro.CAR_ID }, carro);
        }

        // DELETE: api/CARRO/5
        [ResponseType(typeof(CARRO))]
        public IHttpActionResult DeleteCARRO(int id)
        {
            CARRO carro = db.CARRO.Find(id);
            if (carro == null)
            {
                return NotFound();
            }

            db.CARRO.Remove(carro);
            db.SaveChanges();

            return Ok(carro);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CARROExists(int id)
        {
            return db.CARRO.Count(e => e.CAR_ID == id) > 0;
        }
    }
}