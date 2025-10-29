using AutoMapper;
using TrainingManage.Api.Models.Expense;
using TrainingManage.Api.Models.ExpenseParticipant;
using TrainingManage.Api.Models.Person;
using TrainingManage.Api.Models.Registration;
using TrainingManage.Api.Models.Training;
using TrainingManage.Data.Models;

namespace TrainingManage.Api
{
    public class AutomapperConfigurationProfile : Profile
    {
        public AutomapperConfigurationProfile()
        {
            CreateMap<Person, PersonDto>();
            CreateMap<PersonDto, Person>();

            CreateMap<Registration, RegistrationDto>()
            .ForMember(d => d.TrainingTitle, opt => opt.MapFrom(r => r.Training.Title))
            .ForMember(d => d.TrainingDate, opt => opt.MapFrom(r => r.Training.Date))
            .ForMember(d => d.PersonName, opt => opt.MapFrom(r => r.Person.Name))
            ;
            CreateMap<Person, PersonDetailDto>()
                .ForMember(d => d.Person, opt => opt.MapFrom(src => src))      
                .ForMember(d => d.Registrations, opt => opt.MapFrom(src => src.Registrations));

            CreateMap<Training, TrainingDto>();
            CreateMap<TrainingDto, Training>();

            // 1) Základní Expense → ExpenseDto (seznam)
            CreateMap<Expense, ExpenseDto>()
                .ForMember(dest => dest.ParticipantShares,
                           opt => opt.MapFrom(src => src.Participants));

            // 2) ExpenseParticipant → ParticipantShareDto (pokud ExpenseDto používá ParticipantShareDto)
            CreateMap<ExpenseParticipant, ParticipantShareDto>()
                .ForMember(d => d.PersonId,
                           o => o.MapFrom(s => s.PersonId))
                .ForMember(d => d.PersonName,
                           o => o.MapFrom(s => s.Person.Name))
                .ForMember(d => d.ShareAmount,
                           o => o.MapFrom(s => s.ShareAmount));

            // 3) ExpenseParticipant → ExpenseParticipantDto 
            CreateMap<ExpenseParticipant, ExpenseParticipantDto>()
                .ForMember(d => d.ExpenseId,
                           o => o.MapFrom(s => s.ExpenseId))
                .ForMember(d => d.PersonId,
                           o => o.MapFrom(s => s.PersonId))
                .ForMember(d => d.PersonName,
                           o => o.MapFrom(s => s.Person.Name))
                .ForMember(d => d.ShareAmount,
                           o => o.MapFrom(s => s.ShareAmount));

            // 4) DTO pro vytváření → Expense
            CreateMap<ExpenseCreateDto, Expense>()
                .ForMember(dest => dest.Date,
                           opt => opt.MapFrom(_ => DateTime.UtcNow));

            // Registrace
            CreateMap<RegistrationCreateDto, Registration>()
                .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.Payment));

            //CreateMap<Registration, RegistrationDto>();

            // Transakce
            //CreateMap<PersonTransaction, PersonTransactionDto>();
     
            CreateMap<TrainingCreateDto, Training>();


        }
    }
}
