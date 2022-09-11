import React from 'react';
import './App.css';
import { ToDoAdd } from './components/todo-add-component';
import { ToDoList } from './components/todo-list-component';
import { ToDoService } from './services/todo-service';

var childRef = React.createRef<ToDoList>();
var todoService = new ToDoService();

function refreshLists() {    
  if (!childRef.current) {
    return;
  }
    childRef.current.update();
};

function App() {
  return (
    <div className="App">
        <header className="App-body">
            <ToDoList ref={childRef} service={todoService} />
            <ToDoAdd service={todoService} onAddSuccessful={refreshLists} /> 
        </header>
    </div>
  );
}

export default App;
