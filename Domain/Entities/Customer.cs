using StageTest.Domain.ValueObjects;
using System.Net;

namespace StageTest.Domain.Entities;
public class Customer
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Address Address { get; private set; }

    public Customer(Guid id, string name, Address address)
    {
        Id = id;
        Name = name;
        Address = address;
    }

    public void ChangeAddress(Address newAddress)
    {
        Address = newAddress;
    }
}
