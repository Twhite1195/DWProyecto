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
            PROVEEDOR pROVEEDOR = db.PROVEEDOR.Find(id);
            if (pROVEEDOR == null)
            {
                return NotFound();
            }

            return Ok(pROVEEDOR);
        }

        // PUT: api/PROVEEDOR/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPROVEEDOR(int id, PROVEEDOR pROVEEDOR)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pROVEEDOR.PROVE_ID)
            {
                return BadRequest();
            }

            db.Entry(pROVEEDOR).State = EntityState.Modified;

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
        public IHttpActionResult PostPROVEEDOR(PROVEEDOR pROVEEDOR)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PROVEEDOR.Add(pROVEEDOR);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PROVEEDORExists(pROVEEDOR.PROVE_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pROVEEDOR.PROVE_ID }, pROVEEDOR);
        }

        // DELETE: api/PROVEEDOR/5
        [ResponseType(typeof(PROVEEDOR))]
        public IHttpActionResult DeletePROVEEDOR(int id)
        {
            PROVEEDOR pROVEEDOR = db.PROVEEDOR.Find(id);
            if (pROVEEDOR == null)
            {
                return NotFound();
            }

            db.PROVEEDOR.Remove(pROVEEDOR);
            db.SaveChanges();

            return Ok(pROVEEDOR);
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