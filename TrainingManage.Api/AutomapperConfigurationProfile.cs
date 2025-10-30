using AutoMapper;
using TrainingManage.Api.Models.Expense;
using TrainingManage.Api.Models.ExpenseParticipant;
using TrainingManage.Api.Models.Person;
using TrainingManage.Api.Models.Registration;
using TrainingManage.Api.Models.Training;
using TrainingManage.Data.Models;

namespace TrainingManage.Api
{
    /// <summary>
    /// Konfigurace AutoMapper mapování mezi entitami a DTO.
    /// </summary>
    public class AutomapperConfigurationProfile : Profile
    {
        public AutomapperConfigurationProfile()
        {
            // Person
            CreateMap<Person, PersonDto>();
            CreateMap<PersonDto, Person>();

            // PersonDetail (obsahuje Person + Registrace)
            CreateMap<Person, PersonDetailDto>()
                .ForMember(d => d.Person, opt => opt.MapFrom(src => src))
                .ForMember(d => d.Registrations, opt => opt.MapFrom(src => src.Registrations));

            // Registration
            // Mapujeme i názvy a datum tréninku a jméno osoby (pokud jsou navigace načteny)
            CreateMap<Registration, RegistrationDto>()
                .ForMember(d => d.TrainingTitle, opt => opt.MapFrom(r => r.Training != null ? r.Training.Title : null))
                .ForMember(d => d.TrainingDate, opt => opt.MapFrom(r => r.Training != null ? r.Training.Date : default))
                .ForMember(d => d.PersonName, opt => opt.MapFrom(r => r.Person != null ? r.Person.Name : null));

            // DTO used to create Registration -> entity
            CreateMap<RegistrationCreateDto, Registration>()
                .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.Payment));

            // Training
            CreateMap<Training, TrainingDto>();
            CreateMap<TrainingDto, Training>();

            // TrainingCreate DTO -> Training entity
            CreateMap<TrainingCreateDto, Training>();

            // Expense -> ExpenseDto (seznam)
            CreateMap<Expense, ExpenseDto>()
                .ForMember(dest => dest.ParticipantShares,
                           opt => opt.MapFrom(src => src.Participants));

            // ExpenseParticipant -> ParticipantShareDto (zobrazení podílu)
            CreateMap<ExpenseParticipant, ParticipantShareDto>()
                .ForMember(d => d.PersonId,
                           o => o.MapFrom(s => s.PersonId))
                .ForMember(d => d.PersonName,
                           o => o.MapFrom(s => s.Person != null ? s.Person.Name : null))
                .ForMember(d => d.ShareAmount,
                           o => o.MapFrom(s => s.ShareAmount));

            // ExpenseParticipant -> ExpenseParticipantDto (detail)
            CreateMap<ExpenseParticipant, ExpenseParticipantDto>()
                .ForMember(d => d.ExpenseId,
                           o => o.MapFrom(s => s.ExpenseId))
                .ForMember(d => d.PersonId,
                           o => o.MapFrom(s => s.PersonId))
                .ForMember(d => d.PersonName,
                           o => o.MapFrom(s => s.Person != null ? s.Person.Name : null))
                .ForMember(d => d.ShareAmount,
                           o => o.MapFrom(s => s.ShareAmount));

            // DTO pro vytváření Expense -> Expense entity (nastavení data server-side)
            CreateMap<ExpenseCreateDto, Expense>()
                .ForMember(dest => dest.Date,
                           opt => opt.MapFrom(_ => DateTime.UtcNow));
        }
    }
}
