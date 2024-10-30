using Contacts.Domain.Contacts.Models;
using Contacts.Domain.Contacts.Repositories;
using Contacts.Domain.Contacts.Services;
using Contacts.Domain.Contacts.VOs;
using Contacts.Infrastructure.Services;
using System.Text.RegularExpressions;

namespace Contacts.Application.Contacts.Services;

public class ContactService
    : ServiceBase<Contact, IContactRepository>, IContactService
{
    public ContactService(IContactRepository repository)
        : base(repository)
    {
    }

    public IList<ContactVO> List()
    {
        return Repository.Query(tracking: false)
            .Select(ContactVO.Cast).ToList();
    }

    public IList<ContactVO> ListByDDD(string ddd)
    {
        return Repository.Query(tracking: false).Where(r => r.Phone.DDD == ddd)
            .Select(ContactVO.Cast).ToList();
    }

    public void Create(ContactVO vo)
    {
        EnsureValidation(vo);

        var entity = ContactVO.Cast(vo);
        Repository.Create(entity);
    }

    public void Update(ContactVO vo)
    {
        EnsureValidation(vo);

        var entity = Repository.GetById(vo.Id);
        var updatedEntity = ContactVO.Cast(vo, entity);

        Repository.Update(updatedEntity);
    }

    public void Delete(Guid id)
    {
        Repository.Delete(id);
    }

    private void EnsureValidation(ContactVO vo)
    {
        //CHECK IF REQUIRED DATA IS FILLED

        var nameIsEmpty = string.IsNullOrWhiteSpace(vo.Name);
        if (nameIsEmpty)
            throw new ArgumentException("Name shouldn't be empty!");

        var phoneDDDIsEmpty = string.IsNullOrWhiteSpace(vo.PhoneDDD);
        if (phoneDDDIsEmpty)
            throw new ArgumentException("Phone DDD shouldn't be empty!");

        var phoneNumberIsEmpty = string.IsNullOrWhiteSpace(vo.PhoneNumber);
        if (phoneNumberIsEmpty)
            throw new ArgumentException("Phone Number shouldn't be empty!");

        var phoneNumberContainsOnlyNumbers = vo.PhoneNumber.All(r => char.IsNumber(r));
        if (!phoneNumberContainsOnlyNumbers)
            throw new ArgumentException("Phone Number should have only numbers!");

        var emailIsEmpty = string.IsNullOrWhiteSpace(vo.EmailAddress);
        if (emailIsEmpty)
            throw new ArgumentException("Email Address shouldn't be empty!");

        //CHECK IF DATA IS VALID

        var phoneDDDIsInvalid = !ValidatePhoneDDD(vo.PhoneDDD);
        if (phoneDDDIsInvalid)
            throw new ArgumentException("Phone DDD is invalid!");

        var phoneNumberIsInvalid = !ValidatePhoneNumber(vo.PhoneNumber);
        if (phoneNumberIsInvalid)
            throw new ArgumentException("Phone Number is invalid!");

        var emailIsInvalid = !ValidateEmailAddress(vo.EmailAddress);
        if (emailIsInvalid)
            throw new ArgumentException("Email Address is invalid!");

        //CHECK IF DATA IS ALREADY IN USE

        var nameAlreadyInUse = Repository.ContactNameAlreadyExists(vo.Name, vo.Id);
        if (nameAlreadyInUse)
            throw new ArgumentException("Name already in use!");

        var phoneAlreadyInUse = Repository.ContactPhoneAlreadyExists(vo.PhoneDDD, vo.PhoneNumber, vo.Id);
        if (phoneAlreadyInUse)
            throw new ArgumentException("Phone already in use!");

        var emailAlreadyInUse = Repository.ContactEmailAlreadyExists(vo.EmailAddress, vo.Id);
        if (emailAlreadyInUse)
            throw new ArgumentException("Email already in use!");
    }

    private static bool ValidatePhoneDDD(string phoneDDD)
    {
        var validDDDs = new List<string>()
        {
            "11", "12", "13", "14", "15", "16", "17", "18", "19",
            "21", "22", "24", "27", "28",
            "31", "32", "33", "34", "35", "37", "38",
            "41", "42", "43", "44", "45", "46", "47", "48", "49",
            "51", "53", "54", "55",
            "61", "62", "64", "63",
            "65", "66", "67",
            "68", "69",
            "71", "73", "74", "75", "77", "79",
            "81", "82", "83", "84", "85", "86", "87", "88", "89",
            "91", "92", "93", "94", "95", "96", "97", "98", "99"
        };

        return validDDDs.Contains(phoneDDD);
    }

    private static bool ValidatePhoneNumber(string phoneNumber)
    {
        string mobilePhonePattern = @"^9\d{8}$";
        string fixedPhonePattern = @"^[2-5]\d{7}$";

        return Regex.IsMatch(phoneNumber, mobilePhonePattern) || Regex.IsMatch(phoneNumber, fixedPhonePattern);
    }

    private static bool ValidateEmailAddress(string emailAddress)
    {
        string emailPattern = @"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(emailAddress, emailPattern);
    }
}
