using Core.Models.ToDo;
using Newtonsoft.Json;
using System.Text;

namespace UI;

public static class Requests
{
    public static async Task<IEnumerable<ToDo>> Get(HttpClient http)
    {
        var response = (await http.GetAsync($"todo")).Content.ReadAsStringAsync().Result;
        var data = JsonConvert.DeserializeObject<IEnumerable<ToDo>>(response);

        return data!;
    }

    public static async Task<ToDo> Get(HttpClient http, int id)
    {
        var response = await http.GetAsync($"todo/{id}");
        var toDo = JsonConvert.DeserializeObject<ToDo>(response.Content.ReadAsStringAsync().Result);

        return toDo!;
    }

    public static async void Post(HttpClient http, ToDo toDo)
    {
        StringContent content = new StringContent(JsonConvert.SerializeObject(toDo),
            Encoding.UTF8, "application/json");

        var request = await http.PostAsync("todo", content);
    }

    public static async void Put(HttpClient http, ToDo toDo)
    {
        StringContent content = new StringContent(JsonConvert.SerializeObject(toDo),
            Encoding.UTF8, "application/json");

        var request = await http.PutAsync("todo", content);
    }

    public static async void Delete(HttpClient http, int id)
    {
        var request = await http.DeleteAsync($"todo/{id}");
    }
}
