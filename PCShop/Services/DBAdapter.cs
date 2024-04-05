using Microsoft.AspNetCore.Mvc;
using PCShop.Models;
using PCShop.Models.Request;
using PCShop.Models.Response;

namespace PCShop.Services
{
    public class DBAdapter
    {
        private static DBAdapter instance;
        private DBAdapter()
        {
        }

        public static DBAdapter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DBAdapter();
                }
                return instance;
            }
        }

        public List<JsonResult> GetPCByParam(string keyword,int page,int limit)
        {
            try
            {
                using (var entity = new DBContext())
                {
                    List<JsonResult> responses = new List<JsonResult>();
                    List<Pc> pcs = entity.Pcs.ToList();
                    for (int i = 0; i < pcs.Count; i++)
                    {
                        Pc pc = pcs[i];
                        FilterResponse response = new FilterResponse();
                        response.Memory = pc.Memory.ToString() + GetStorageUnitById(pc.MemoryUnit.Value);
                        response.Storage = pc.StorageCap.ToString() + GetStorageUnitById(pc.StorageUnit.Value) + " " + GetStorageTypeById(pc.StorageType.Value);
                        if (pc.NumUsb2 >= 0)
                            response.Port += pc.NumUsb2.ToString() + " x " + "USB 2.0, ";
                        if (pc.NumUsb3 >= 0)
                            response.Port += pc.NumUsb2.ToString() + " x " + "USB 3.0, ";
                        if (pc.NumUsbC >= 0)
                            response.Port += pc.NumUsb2.ToString() + " x " + "USB C";
                        response.Render = GetRenderById(pc.RenderId.Value);
                        response.Weight = pc.Weight.ToString() + " " + GetWeightUnitById(pc.WeightUnit.Value);
                        response.PSU = pc.Psu.ToString() + " W PSU";
                        response.Processor = GetProcessorById(pc.ProcessorId.Value);
                        response.ID = pc.Id;
                        if(keyword == "" && i >= limit * page && i < limit * (page + 1))
                            responses.Add(response.toJSON());
                        else if(keyword != "" && response.contains(keyword))
                            responses.Add(response.toJSON());
                    }
                    if (keyword != "")
                        return responses.Slice(0, limit > responses.Count() ? responses.Count() : limit) ;
                    else return responses;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it in any other way
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Rethrow the exception to propagate it further if necessary
            }
        }
        public string UpdatePCByParam(EditRequest requestData)
        {

            //var requestData = JsonConvert.DeserializeObject<EditRequest>(param);
            try
            {
                using (var entity = new DBContext())
                {
                    Pc pc = entity.Pcs.Where(t => t.Id == requestData.id).FirstOrDefault();
                    FilterResponse response = new FilterResponse();
                    response.Memory = pc.Memory.ToString() + GetStorageUnitById(pc.MemoryUnit.Value);
                    response.Storage = pc.StorageCap.ToString() + GetStorageUnitById(pc.StorageUnit.Value) + " " + GetStorageTypeById(pc.StorageType.Value);
                    if (pc.NumUsb2 >= 0)
                        response.Port += pc.NumUsb2.ToString() + " x " + "USB 2.0, ";
                    if (pc.NumUsb3 >= 0)
                        response.Port += pc.NumUsb2.ToString() + " x " + "USB 3.0, ";
                    if (pc.NumUsbC >= 0)
                        response.Port += pc.NumUsb2.ToString() + " x " + "USB C";
                    response.Render = GetRenderById(pc.RenderId.Value);
                    response.Weight = pc.Weight.ToString() + " " + GetWeightUnitById(pc.WeightUnit.Value);
                    response.PSU = pc.Psu.ToString() + " W PSU";
                    response.Processor = GetProcessorById(pc.ProcessorId.Value);
                    response.ID = pc.Id;
                    if(response.Render != requestData.render)
                    {
                        SetRenderByID(pc.Id, requestData.render);
                    }
                    if(response.Processor != requestData.processor)
                    {
                        SetProcessorByID(pc.Id, requestData.processor);
                    }
                    return "success";
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it in any other way
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Rethrow the exception to propagate it further if necessary
            }
        }
        public int GetTotalPCCount(string keyword)
        {
            try
            {
                using (var entity = new DBContext())
                {
                    int count = 0;
                    List<Pc> pcs = entity.Pcs.ToList();
                    //if (pcs.Count == 0)
                    //    return new List<string>();
                    for (int i = 0; i < pcs.Count; i++)
                    {
                        Pc pc = pcs[i];
                        FilterResponse response = new FilterResponse();
                        response.Memory = pc.Memory.ToString() + GetStorageUnitById(pc.MemoryUnit.Value);
                        response.Storage = pc.StorageCap.ToString() + GetStorageUnitById(pc.StorageUnit.Value) + " " + GetStorageTypeById(pc.StorageType.Value);
                        if (pc.NumUsb2 >= 0)
                            response.Port += pc.NumUsb2.ToString() + " x " + "USB 2.0, ";
                        if (pc.NumUsb3 >= 0)
                            response.Port += pc.NumUsb2.ToString() + " x " + "USB 3.0, ";
                        if (pc.NumUsbC >= 0)
                            response.Port += pc.NumUsb2.ToString() + " x " + "USB C";
                        response.Render = GetRenderById(pc.RenderId.Value);
                        response.Weight = pc.Weight.ToString() + " " + GetWeightUnitById(pc.WeightUnit.Value);
                        response.PSU = pc.Psu.ToString() + " W PSU";
                        response.Processor = GetProcessorById(pc.ProcessorId.Value);
                        response.ID = pc.Id;
                        if (response.contains(keyword))
                            count++;
                    }
                    return count;
                }
            }
            catch(Exception ex)
            {
                return 0;
            }
        }
        public string GetStorageUnitById(int id)
        {
            try
            {
                using (var entity = new DBContext())
                {
                    return entity.StorageUnits.Where(t => t.Id == id).FirstOrDefault().Unit;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it in any other way
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Rethrow the exception to propagate it further if necessary
            }
        }
        public string GetStorageTypeById(int id)
        {
            try
            {
                using (var entity = new DBContext())
                {
                    return entity.StorageTypes.Where(t => t.Id == id).FirstOrDefault().Type;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it in any other way
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Rethrow the exception to propagate it further if necessary
            }
        }
        public string GetRenderById(int id)
        {
            try
            {
                using (var entity = new DBContext())
                {
                    return entity.Renders.Where(t => t.Id == id).FirstOrDefault().Name;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it in any other way
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Rethrow the exception to propagate it further if necessary
            }
        }
        public bool SetRenderByID(int id,string render)
        {
            try
            {
                using (var entity = new DBContext())
                {
                    Render renderer = entity.Renders.Where(t => t.Id == id).FirstOrDefault();
                    renderer.Name = render;
                    entity.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it in any other way
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
                throw; // Rethrow the exception to propagate it further if necessary
            }
        }
        public string GetProcessorById(int id)
        {
            try
            {
                using (var entity = new DBContext())
                {
                    return entity.Processors.Where(t => t.Id == id).FirstOrDefault().Name;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it in any other way
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Rethrow the exception to propagate it further if necessary
            }
        }
        public bool SetProcessorByID(int id,string processor)
        {
            try
            {
                using (var entity = new DBContext())
                {
                    Processor proc  =entity.Processors.Where(t => t.Id == id).FirstOrDefault();
                    proc.Name = processor;
                    entity.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it in any other way
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Rethrow the exception to propagate it further if necessary
                return false;
            }
        }
        public string GetWeightUnitById(int id)
        {
            try
            {
                using (var entity = new DBContext())
                {
                    return entity.WeightUnits.Where(t => t.Id == id).FirstOrDefault().Unit;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it in any other way
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Rethrow the exception to propagate it further if necessary
            }
        }
        //public User UpdatePCByParam(string param)
        //{
        //    var requestData = JsonConvert.DeserializeObject<GetRequest>(param);

        //}
    }
}
