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
    [RoutePrefix("api/PROVEEDOR")]
    public class PROVEEDORController : ApiController
    {
        private ProyectoEntities db = new ProyectoEntities();

        // GET: api/PROVEEDOR
        public IQueryable<PROVEEDOR> GetPROVEEDOR()
        {
            return db.PROVEEDOR;
        }

        // GET: api/PROVEEDOR/5
        [ResponseType(typeof(PROVEEDOR))]
        public IHttpActionResult GetPROVEEDOR(int id)
        {
            PROVEEDOR proveedor = db.PROVEEDOR.Find(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            return Ok(proveedor);
        }

        // PUT: api/PROVEEDOR/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPROVEEDOR(int id, PROVEEDOR proveedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != proveedor.PROVE_ID)
            {
                return BadRequest();
            }

            db.Entry(proveedor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PROVEEDORExists(id))
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

        // POST: api/PROVEEDOR
        [ResponseType(typeof(PROVEEDOR))]
        public IHttpActionResult PostPROVEEDOR(PROVEEDOR proveedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PROVEEDOR.Add(proveedor);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PROVEEDORExists(proveedor.PROVE_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = proveedor.PROVE_ID }, proveedor);
        }

        // DELETE: api/PROVEEDOR/5
        [ResponseType(typeof(PROVEEDOR))]
        public IHttpActionResult DeletePROVEEDOR(int id)
        {
            PROVEEDOR proveedor = db.PROVEEDOR.Find(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            db.PROVEEDOR.Remove(proveedor);
            db.SaveChanges();

            return Ok(proveedor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PROVEEDORExists(int id)
        {
            return db.PROVEEDOR.Count(e => e.PROVE_ID == id) > 0;
        }
    }
}