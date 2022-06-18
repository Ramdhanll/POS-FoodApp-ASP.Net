function downloadExcel(url, nameFile) {
    fetch(url, {
        method: 'GET',
        headers: new Headers({
            "Authorization": `Bearer ${localStorage.getItem('authToken').replace(/"/g, "")}`
        })
    })
        .then(response => response.blob())
        .then(blob => {
            var url = window.URL.createObjectURL(blob);
            var a = document.createElement('a');
            a.href = url;
            a.download = `${nameFile}.xlsx`;
            document.body.appendChild(a); // we need to append the element to the dom -> otherwise it will not work in firefox
            a.click();
            a.remove();  //afterwards we remove the element again         
        });
}

function downloadCSV(url, nameFile) {
    fetch(url, {
        method: 'GET',
        headers: new Headers({
            "Authorization": `Bearer ${localStorage.getItem('authToken').replace(/"/g, "")}`
        })
    })
        .then(response => response.blob())
        .then(blob => {
            var url = window.URL.createObjectURL(blob);
            var a = document.createElement('a');
            a.href = url;
            a.download = `${nameFile}.csv`;
            document.body.appendChild(a); // we need to append the element to the dom -> otherwise it will not work in firefox
            a.click();
            a.remove();  //afterwards we remove the element again         
        });
}