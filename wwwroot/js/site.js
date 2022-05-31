async function getUsers()
{
    const response = await fetch("https://localhost:7035/api/Home",{
    method:"GET"
    })
    .then(async res=>await res.json());
    var ul = document.getElementById("ul");
    response.forEach(element => {
        console.log(element);
        var label = document.createElement("label");
        label.innerHTML+=element.name;
        var li = document.createElement("li");
        li.appendChild(label);
        ul.appendChild(li);        
    });
}
