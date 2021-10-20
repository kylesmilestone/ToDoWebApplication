import React, { Component } from 'react';
import queryString from 'query-string';

export class ToDoEditor extends Component {
    static displayName = ToDoEditor.name;

    constructor(props) {
        super(props);
        this.state = { title: '', toDoState: 'todo', id: '', loading: true };
    }

    componentDidMount() {
        this.populateTodoData();
    }

    async populateTodoData() {
        let params = queryString.parse(this.props.location.search)
        var id = params.id;
        if (id) {
            // TODO: compare fetch and XMLHttpRequest and see which one has better performance
            const response = await fetch(`api/todo/${id}`);
            const data = await response.json();
            this.setState({ toDoState: data.toDoState, title: data.title, id: data.id, loading: false });
        } else {
            this.setState({ title: 'new title', loading: false });
        }
    }

    handleTitleChange = (event) => {
        this.setState({ title: event.target.value });
    }

    handleTodoStateChange = (event) => {
        this.setState({ toDoState: event.target.value });
    }

    handleClick = (event) => {
        const requestOptions = {
            method: 'DELETE'
        };
        fetch("/api/todo/" + this.state.id, requestOptions).then((response) => {
            this.props.history.push('/');
        });
    }

    onstatechange = () => {
        this.props.history.push('/');
    }

    handleSubmit = (event) => {
        console.log('clicked');
        console.log(this.state.title);
        console.log(this.state.toDoState);

        // TODO: compare fetch and XMLHttpRequest and see which one has better performance
        if (!this.state.id || this.state.id === '') {
            var request = new XMLHttpRequest();
            request.open('POST', '/api/todo/create', true);
            request.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
            request.onreadystatechange = this.onstatechange;
            request.send(JSON.stringify({ title: this.state.title, toDoState: this.state.toDoState }));
        } else {
            var request = new XMLHttpRequest();
            request.open('POST', '/api/todo/update', true);
            request.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
            request.onreadystatechange = this.onstatechange;
            request.send(JSON.stringify({ title: this.state.title, toDoState: this.state.toDoState, id: this.state.id }));
        }
        event.preventDefault();
    }

    render() {
        return (
            <div>
                <form onSubmit={this.handleSubmit}>
                    <select value={this.state.toDoState} onChange={this.handleTodoStateChange}>
                        <option value="todo">Todo</option>
                        <option value="doing">Doing</option>
                        <option value="done">Done</option>
                    </select>
                    <br />
                    <br />
                    <label>
                        Title:
                        <input type="text" value={this.state.title} onChange={this.handleTitleChange}/>
                    </label>
                    <br />
                    <br />
                    <input type="submit" value="Submit" />
                </form>
                <br />
                { this.state.id != '' &&
                    <button onClick={this.handleClick}>
                        Delete
                    </button>
                }
            </div>
        );
    }
}
