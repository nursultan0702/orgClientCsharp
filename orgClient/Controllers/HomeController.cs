using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using orgClient.StructureService;
using orgClient.Models;
using System.Net;
using System.Net.Sockets;
using System.Web.Services.Protocols;

namespace orgClient.Controllers
{
    public class HomeController : Controller
    {
        public StructureClient client = new StructureClient();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult UnitList()
        {
            UnitModel model = new UnitModel { UnitList = GetUnitByParentID(getToken(), 0).ToList<unit>() };
            return PartialView("UnitList", model);
        }
        [HttpGet]
        public ActionResult UnitListbyParent(string unitId)
        {
            try
            {
                UnitModel model = new UnitModel { UnitList = GetUnitByParentID(getToken(), Int32.Parse(unitId)).ToList<unit>() };
                return PartialView("UnitList", model);
            }
            catch (ArgumentNullException ex) {
                return PartialView("Error");
            }
        }
        public ActionResult PersonList(string unitId)
        {
            try {
                position[] pos = GetPositionsbyUnit(getToken(), Int32.Parse(unitId));
                List<position> list = pos.ToList<position>();
                PositionModel model = new PositionModel { PositionList = list };
                return PartialView("PersonList",model);
            } catch(Exception ex) {
                return PartialView("Error");
                    }
        }
        public ActionResult ModalWindow(string personid,string parentpositionid)
        {
            try {
                PositionModel model = new PositionModel { position = client.getPositionById(getToken(), Int32.Parse(personid)),
                                                          parentPosition = client.getPositionById(getToken(), Int32.Parse(parentpositionid))};
                return PartialView("Modal", model);
            } catch (Exception ex) {
                return PartialView("Error");
            } }
        public ActionResult PersonInfo(string positionid, string parentpositionid)
        {
            try {
                position position = client.getPositionById(getToken(), Int32.Parse(positionid));
                if (Int32.Parse(parentpositionid) != 0)
                {
                    position parentPosition = client.getPositionById(getToken(), Int32.Parse(parentpositionid));
                    return View("PersonInfo", new PositionModel
                    {
                        position = position,
                        parentPosition = parentPosition
                    });
                }
                else
                {
                    return View("PersonInfo", new PositionModel
                    {
                        position = position
                    });
                }
            } catch(Exception ex)
            {
               return PersonInfo(positionid,"0");
            }
        }
        public ActionResult Search(string searchtext)
        {
            try
            {
                position[] posit = client.findPositionByName(getToken(), searchtext);
                PositionModel model = new PositionModel
                {
                    PositionList = posit.ToList<position>()
                };
                return PartialView("Search", model);
            }catch(Exception ex)
            {
                return PartialView("Error");
            }
        }
        public String getToken()
        {
            String[] name = User.Identity.Name.Split('\\');
            String ipadress = GetLocalIPAddress();
            //return Base64Encode(name[1]+"_"+ipadress);
            return "QVllc21ha2hhbm92XzE3Mi4xMjguMi4xMA==";
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        [HttpPost]
        public unit[] GetUnitByParentID(String userId,int parentId)
        {
            try {
                return client.getUnitListByParentID(userId, parentId);
                } catch(SoapException error)
            {
                return null;
            }
        }
        public unit[] GetUnitByParentID(String userId)
        {
                return client.getUnitListByParentID(userId, 0);
            
        }
        public position[] GetPositionsbyUnit(String userId,int unitId)
        {
            try
            {
                return client.getPositionListByUnitId(userId, unitId);
            } catch(Exception ex) { return null; }
        }
        public String CreatePosition(String userId, position position)
        {
            try
            {
                return client.createPosition(userId, position);
            }catch(Exception ex) { return null; }
        }
        public String CreateUnit(String userId,unit unit)
        {
            try
            {
                return client.createUnit(userId, unit);
            }catch(Exception ex) { return null; }
        }
        public String DeletePosition(String userId, position position)
        {
            try
            {
                return client.deletePosition(userId, position.POSITIONID);
            }catch(Exception ex) { return null; }
        }
        public String DeleteUnit(String userId, unit unit)
        {
            try
            {
                return client.deleteUnit(userId, unit.UNITID);
            }catch(Exception ex) { return null; }
        }
        public String UpdatePostion(String userId,position position)
        {
            try
            {
                return client.updatePosition(userId, position);
            }catch(Exception ex) { return null; }
        }
        public String UpdateUnit(String userId,unit unit)
        {
            try
            {
                return client.updateUnit(userId, unit);
            }catch(Exception ex) { return null; }
        }
    }
}