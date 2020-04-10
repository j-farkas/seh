import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <form>
          <label>Title:
            <input type = 'text' id ='title' name = 'title'/>
          </label>
          <br></br>
          <label>Text:
            <textarea type = 'textarea' rows = '5'
              cols = '65' id ='text' name = 'text'/>
          </label>
        </form>
      </div>
    );
  }
}
