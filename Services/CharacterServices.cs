using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace sample.rpg.Services
{
    public class CharacterServices : ICharacterServices
    {
          private static List<Character>characters=new List<Character>
       {
         new Character(),
         new Character{
            Id=2,
            Name="akhil"
         }
       };
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterServices(IMapper mapper ,DataContext context)
       {
           _mapper = mapper;
            _context = context;
        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse =new ServiceResponse<List<GetCharacterDto>>();
            var character=_mapper.Map<Character>(newCharacter);
            
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            serviceResponse.Data=await _context.Characters.Select(c=>_mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse=new ServiceResponse<List<GetCharacterDto>>();
             var DbCharacters=await _context.Characters.FirstAsync(c=>c.Id==id);
        _context.Characters.Remove(DbCharacters);
        await _context.SaveChangesAsync();
        serviceResponse.Data=await _context.Characters.Select(c=>_mapper.Map<GetCharacterDto>(c)).ToListAsync();
        return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
             var serviceResponse=new ServiceResponse<GetCharacterDto>();
            var DbCharacters=await _context.Characters.FirstOrDefaultAsync(c=>c.Id==id);
            serviceResponse.Data=_mapper.Map<GetCharacterDto>(DbCharacters);
            return serviceResponse;
             
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetCharacters()
        {
            var serviceResponse=new ServiceResponse<List<GetCharacterDto>>();
            var DbCharacter=await _context.Characters.ToListAsync();
            serviceResponse.Data=DbCharacter.Select(c=>_mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> Updatecharacter(UpdateCharacterDto updateCharacter)
         {
             var serviceResponse=new ServiceResponse<GetCharacterDto>();
            //  try{

             
            var DbCharacters= await _context.Characters.FirstOrDefaultAsync(c=>c.Id==updateCharacter.Id);
            
               if(DbCharacters is null)
                  throw new Exception($"The character id '{updateCharacter.Id}' is not found");
                  _mapper.Map(updateCharacter,DbCharacters);
                await _context.SaveChangesAsync();
            //  cha.Id= updateCharacter.Id;

            //  cha.Name=updateCharacter.Name;

            serviceResponse.Data = _mapper.Map<GetCharacterDto>(DbCharacters);
            //  }
            //  catch (Exception ex)
            //  {
            //     serviceResponse.Success=false;
            //     serviceResponse.Message=ex.Message;

            //  }
      

         return serviceResponse;
         }
    }
}