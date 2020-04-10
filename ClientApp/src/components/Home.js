import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  constructor (props) {
    super(props);
    this.state = { images: [], title: ""};
    this.getImages = this.getImages.bind(this);
    this.handleChange = this.handleChange.bind(this);
  }

handleChange(event){
  this.setState({title: event.target.value})
  console.log(this.state);
}

getImages(words){
  fetch('api/SampleData/ImageList/'+words)
    .then(response => response.json())
    .then(data => {
      this.setState({ images: data});
      console.log(this.state);
    });
}


  render () {
    return (
      <div>
        <form>
          <label>Title:
            <input type = 'text' id ='title' name = 'title' value = {this.state.title} onChange={this.handleChange}/>
          </label>
          <br></br>
          <label>Text:
            <textarea type = 'textarea' rows = '5'
              cols = '65' id ='text' name = 'text'/>
          </label>
          <h3 onClick={()=>this.getImages(this.state.title)}>Submit</h3>
        </form>
      </div>
    );
  }
}
