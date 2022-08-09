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

        public QuestsController(IHttpClientFactory clientFactory, QuestContext context)
        {
            _context = context;
            if (clientFactory is null)
            {
                throw new ArgumentNullException(nameof(clientFactory));
            }
            _client = clientFactory.CreateClient("mhw");
        }

        // GET: api/Quests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quest>>> GetQuest()
        {
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
        public async Task<IActionResult> PutQuest(long id, Quest quest)
        {
            if (id != quest.Id)
            {
                return BadRequest();
            }

            _context.Entry(quest).State = EntityState.Modified;

            try
            {
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

            return NoContent();
        }

        // POST: api/Quests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Quest>> PostQuest(string date)
        {
          if (_context.Quest == null)
          {
              return Problem("Entity set 'QuestContext.Quest'  is null.");
          }
            string[] dateSplit = date.Split('-');
            Quest quest = new Quest();
            String dayNum = dateSplit[0].TrimStart(new Char[] {'0'});
            String monthNum = dateSplit[1].TrimStart(new Char[] {'0'});

            var monsterRes = await _client.GetAsync("/monsters/" + dayNum);
            var monsterContent = await monsterRes.Content.ReadAsStringAsync();
            Monster monster = JsonConvert.DeserializeObject<Monster>(monsterContent);

            var armorRes = await _client.GetAsync("/armor/sets/" + monthNum);
            var armorContent = await armorRes.Content.ReadAsStringAsync();
            ArmorSet armorSet  = JsonConvert.DeserializeObject<ArmorSet>(armorContent);
    

            quest.ArmorSet = $"{armorSet.name}";
            quest.Monster = $"{monster.name}";
            quest.isComplete = false;
            _context.Quest.Add(quest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuest", new { id = quest.Id }, quest);
        }

        // DELETE: api/Quests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuest(long id)
        {
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
