using ContactSystem.Application.DTOs;
using ContactSystem.Application.Services.ContactService;

namespace ContactSystem.Api.Endpoints;

public static class ContactEndpoints
{
    public static void MapContactEndpoints(this WebApplication app)
    {
        var contactGroup = app.MapGroup("/contacts")
        //.RequireAuthorization()
        .WithTags("Contacts");
        contactGroup.MapPost("/post",
            async (ContactCreateDto contactDto, IContactService contactService) =>
            {
                var contactId = await contactService.PostAsync(contactDto);
                return contactId;
            })
            .WithName("CreateContact")
            .Produces(200)
            .Produces(400);

        contactGroup.MapDelete("/delete/{contactId:long}",
            async(long contactId,IContactService contactService)=>
            {
                await contactService.DeleteAsync(contactId);
            })
            .WithName("DeleteContact")
            .Produces(200)
            .Produces(404);

        contactGroup.MapGet("/getAll{skip:int} {take:int}",
           async (int skip,int take,IContactService contactService)=>
            {
                var contacts = contactService.GetAllAsync(skip,take);
                return contacts;
            })
            .WithName("GetAllContacts")
            .Produces(200)
            .Produces(404);

        contactGroup.MapGet("/getById",
            async(long contactId,IContactService contactService)=>
            {
                var contact=await contactService.GetByIdAsync(contactId);
                return contact;
            })
            .WithName("GetContactById")
            .Produces(200)
            .Produces(404);

        contactGroup.MapPut("/update",
            async (ContactDto contactDto, IContactService contactService) =>
            {
                await contactService.UpdateAsync(contactDto);
            })
            .WithName("UpdateContact")
            .Produces(200)
            .Produces(404);





    }
}
