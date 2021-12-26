
const from = document.querySelector('form');
const input = document.querySelector('#addMaterial');
const btnDeleteAll = document.querySelector('#btnDeleteAll');

const taskList = document.querySelector('#task-list');
eventListeners();

function eventListeners() {
    form.addEventListener('submit', addNewItem);
}

function addNewItem(e) {
    
    e.preventDefault();
}