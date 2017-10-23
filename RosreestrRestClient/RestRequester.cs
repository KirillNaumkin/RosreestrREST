using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using RosreestrRestClient.Types;

namespace RosreestrRestClient {
    /// <summary>
    /// Предоставляет набор статических методов для получения от REST-сервиса Росреестра общедоступных сведений об объектах недвижимости и правах на них.
    /// </summary>
    public static class RestRequester {
        
        /// <summary>
        /// Возвращает найденный по кад.номеру объект недвижимости.
        /// </summary>
        /// <param name="cadNum">Кадастровый номер объекта недвижимости.</param>
        /// <returns>Null или объект недвижимости.</returns>
        public static CadastralObject GetByCadNum(string cadNum) {
            CadastralObject result = null;
            if ((cadNum != null) && IsCadNum(cadNum)) {
                string id = CadNumToId(cadNum);
                result = GetById(id);
            }
            return result;
        }

        /// <summary>
        /// Возвращает найденную по ИД запись (о праве или об объекте недвижимости).
        /// </summary>
        /// <param name="id">Идентификатор записи.</param>
        /// <returns>Null или найденная запись.</returns>
        public static CadastralObject GetById(string id) {
            CadastralObject result = null;
            if (id.Length > 0) {
                string jsonContent = RequestJson(RequestType.GetObjectById, id);
                if (jsonContent != null) {
                    result = JsonConvert.DeserializeObject<CadastralObject>(jsonContent);
                }
            }
            return result;
        }

        /// <summary>
        /// Возвращает массив записей Росреестра, найденных по номеру.
        /// </summary>
        /// <param name="number">Кадастровый, условный или устаревший номер объекта, номер права или обременения.</param>
        /// <returns>Массив записей из 0-200 элементов.</returns>
        public static SearchResult[] SearchByNumber(string number) {
            SearchResult[] result = { };
            if (number.Length > 0) {
                string jsonContent = RequestJson(RequestType.SearchObjectsByNumber, number);
                if (jsonContent != null) {
                    result = JsonConvert.DeserializeObject<SearchResult[]>(jsonContent);
                }
            }
            return result;
        }

        /// <summary>
        /// Возвращает массив записей Росреестра, найденных по адресу.
        /// </summary>
        /// <param name="macroRegionId">ИД макро-региона.</param>
        /// <param name="regionId">ИД региона.</param>
        /// <param name="settlementId">ИД населённого пункта.</param>
        /// <param name="streetName">Название улицы, площади, другого геонима.</param>
        /// <param name="houseNum">Номер дома.</param>
        /// <returns>Массив записей из 0-200 элементов.</returns>
        public static SearchResult[] SearchByAddress(string macroRegionId, string regionId = "", string settlementId = "", string streetName = "", string houseNum = "") {
            //throw new Exception("Этот метод ещё не реализован");
            SearchResult[] result = { };
            if ((macroRegionId != null) && (macroRegionId.Length > 0)) {
                string paramString = "macroRegionId=" + macroRegionId;
                if ((regionId       != null) && (regionId.Length     > 0)) { paramString += "&regionId="     + regionId; }
                if ((settlementId   != null) && (settlementId.Length > 0)) { paramString += "&settlementId=" + settlementId; }
                if ((streetName     != null) && (streetName.Length   > 0)) { paramString += "&street="       + streetName; }
                if ((houseNum       != null) && (houseNum.Length     > 0)) { paramString += "&house="        + houseNum; }
                string jsonContent = RequestJson(RequestType.SearchObjectsByAddress, paramString);
                if (jsonContent != null) {
                    result = JsonConvert.DeserializeObject<SearchResult[]>(jsonContent);
                }
            }
            return result;
        }

        /// <summary>
        /// Возвращает список макро-регионов.
        /// </summary>
        /// <returns>Массив записей из 0 или более элементов.</returns>
        public static Region[] GetMacroRegions() {
            Region[] result = { };
            string jsonContent = RequestJson(RequestType.GetMacroRegions, null);
            if (jsonContent != null) {
                result = JsonConvert.DeserializeObject<Region[]>(jsonContent);
            }
            return result;
        }

        /// <summary>
        /// Возвращает список дочерних регионов для региона с указанным кодом.
        /// </summary>
        /// <param name="parentCode">Код региона, для которого нужно получить список дочерних регионов.</param>
        /// <returns>Массив записей из 0 или более элементов.</returns>
        public static Region[] GetChildRegions(string parentCode) {
            Region[] result = { };
            if (parentCode.Length > 0) {
                string jsonContent = RequestJson(RequestType.GetSubRegions, parentCode);
                if (jsonContent != null) {
                    result = JsonConvert.DeserializeObject<Region[]>(jsonContent);
                }
            }
            return result;
        }

        private enum RequestType { GetObjectById, SearchObjectsByNumber, SearchObjectsByAddress, GetMacroRegions, GetSubRegions }
        private static Dictionary<RequestType, String> RequestPatterns = new Dictionary<RequestType, string> {
            { RequestType.GetObjectById,            "http://rosreestr.ru/api/online/fir_object/{ObjectId}" },
            { RequestType.SearchObjectsByNumber,    "http://rosreestr.ru/api/online/fir_objects/{Number}" },
            { RequestType.SearchObjectsByAddress,   "http://rosreestr.ru/api/online/address/fir_objects?{Parameters}" },
            { RequestType.GetMacroRegions,          "http://rosreestr.ru/api/online/macro_regions" },
            { RequestType.GetSubRegions,            "http://rosreestr.ru/api/online/regions/{ParentRegion}" }
        };

        private static string RequestJson(RequestType requestType, object valueToLookFor) {
            WebRequest request = null;
            switch (requestType) {
                case RequestType.GetObjectById:
                    request = WebRequest.Create(RequestPatterns[RequestType.GetObjectById].Replace("{ObjectId}", valueToLookFor as string));
                    break;
                case RequestType.SearchObjectsByNumber:
                    request = WebRequest.Create(RequestPatterns[RequestType.SearchObjectsByNumber].Replace("{Number}", valueToLookFor as string));
                    break;
                case RequestType.SearchObjectsByAddress:
                    request = WebRequest.Create(RequestPatterns[RequestType.SearchObjectsByAddress].Replace("{Parameters}", valueToLookFor as string));
                    break;
                case RequestType.GetMacroRegions:
                    request = WebRequest.Create(RequestPatterns[RequestType.GetMacroRegions]);
                    break;
                case RequestType.GetSubRegions:
                    request = WebRequest.Create(RequestPatterns[RequestType.GetMacroRegions].Replace("{ParentRegion}", valueToLookFor as string));
                    break;
                default:
                    break;
            }
            request.Credentials = CredentialCache.DefaultCredentials;       // Указываем системные учетные данные приложения.
            request.Proxy.Credentials = CredentialCache.DefaultCredentials; // Указываем сетевые учетные данные текущего контекста безопасности (настройки IE, прокси).
            WebResponse response = null;
            try {
                response = request.GetResponse();
            }
            catch (Exception ex) {
                return null;
            }
            StreamReader stream = new StreamReader(response.GetResponseStream());
            string jsonContent = stream.ReadLine();
            stream.Close();
            return jsonContent;
        }

        private static bool IsCadNum(string value) {
            return System.Text.RegularExpressions.Regex.IsMatch(value, "^[0-9]{2}:[0-9]{2}:[0-9]{6,7}:[0-9]{1,}$");
        }

        private static string CadNumToId(string cadNum) {
            var strNumbers = cadNum.Split(':');
            List<long> numbers = new List<long>();
            foreach (var item in strNumbers) {
                numbers.Add(long.Parse(item));
            }
            return string.Join(":", numbers.ToArray());
        }

        /// <summary>
        /// Возвращает объект учёта для текущего результата поиска.
        /// </summary>
        /// <param name="searchRes">Результат поиска, для которого надо получить подробную запись.</param>
        /// <returns>Null или найденная запись.</returns>
        public static CadastralObject GetCadastralObject(this SearchResult searchRes) {
            return GetById(searchRes.ID);
        }

        /// <summary>
        /// Возвращает список дочерних регионов для текущего региона.
        /// </summary>
        /// <param name="reg">Текущий регион.</param>
        /// <returns>Массив записей из 0 или более элементов.</returns>
        public static Region[] GetChildren(this Region reg) {
            return GetChildRegions(reg.ID);
        }
    }
}
