using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MHQuestGenerator.Models;
using Newtonsoft.Json;

namespace MHQuestGenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestsController : ControllerBase
    {
        private readonly HttpClient _client;
        private readonly QuestContext _context;
        private readonly ILogger _logger;

        public QuestsController(IHttpClientFactory clientFactory, QuestContext context, ILogger<QuestsController> logger)
        {
            _context = context;
            if (clientFactory is null)
            {
                throw new ArgumentNullException(nameof(clientFactory));
            }
            _client = clientFactory.CreateClient("mhw");
            _logger = logger;
        }

        // GET: api/Quests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quest>>> GetQuest()
        {
            Console.WriteLine("GET api/Quests");
            _logger.LogDebug("GET api/Quests");
            if (_context.Quest == null)
            {
                return NotFound();
            }
            return await _context.Quest.ToListAsync();
        }

        // GET: api/Quests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Quest>> GetQuest(long id)
        {
            Console.WriteLine("GET api/Quests/id ran");
            _logger.LogDebug("GET api/Quests/id ran");
            if (_context.Quest == null)
            {
                return NotFound();
            }
            var quest = await _context.Quest.FindAsync(id);

            if (quest == null)
            {
                return NotFound();
            }

            return quest;
        }

        // PUT: api/Quests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuest(long id, Boolean isComplete)
        {
            Console.WriteLine("PUT api/Quests/id");
            _logger.LogDebug("PUT api/Quests/id");
            //if (id != quest.Id)
            //{
            //    return BadRequest();
            //}
            var quest = await _context.Quest.FindAsync(id);


            try
            {
                quest.isComplete = isComplete;
                _context.Entry(quest).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetQuest", new { id = quest.Id }, quest);
        }


        public static String[] genDateArray(string date)
        {
            string[] dateSplit = date.Split('-');
            Quest quest = new Quest();
            String dayNum = dateSplit[0].TrimStart(new Char[] { '0' });
            String monthNum = dateSplit[1].TrimStart(new Char[] { '0' });
            String yearNum = dateSplit[2].Substring(dateSplit[2].Length - 2).TrimStart(new Char[] { '0' });
            String[] dateArray = { dayNum, monthNum, yearNum };
            return dateArray;
        }

        // POST: api/Quests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Quest>> PostQuest(string date)
        {
            Console.WriteLine("POST api/Quests/");
            _logger.LogDebug("POST api/Quests/");
            if (_context.Quest == null)
            {
                return Problem("Entity set 'QuestContext.Quest'  is null.");
            }
            string[] dateSplit = date.Split('-');
            Quest quest = new Quest();
            String[] dateArray = genDateArray(date);
            String dayNum = dateArray[0];
            String monthNum = dateArray[1];

            var monsterRes = await _client.GetAsync("/monsters/" + dayNum);
            var monsterContent = await monsterRes.Content.ReadAsStringAsync();
            Monster monster = JsonConvert.DeserializeObject<Monster>(monsterContent);

            var armorRes = await _client.GetAsync("/armor/sets/" + monthNum);
            var armorContent = await armorRes.Content.ReadAsStringAsync();
            ArmorSet armorSet = JsonConvert.DeserializeObject<ArmorSet>(armorContent);


            quest.ArmorSet = $"{armorSet.name}";
            quest.Monster = $"{monster.name}";
            quest.isComplete = false;
            _context.Quest.Add(quest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PutQuest", new { id = quest.Id }, quest);
        }

        // DELETE: api/Quests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuest(long id)
        {
            Console.WriteLine("DELETE api/Quests/");
            _logger.LogDebug("DELETE api/Quests/");
            if (_context.Quest == null)
            {
                return NotFound();
            }
            var quest = await _context.Quest.FindAsync(id);
            if (quest == null)
            {
                return NotFound();
            }

            _context.Quest.Remove(quest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestExists(long id)
        {
            return (_context.Quest?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
