import React from  'react';
class ArtistForm extends React.Component{

    constructor(props) {
        super(props);
        this.state = {id: '', name:'', genre:''};

        //  this.handleChange = this.handleChange.bind(this);
        // this.handleSubmit = this.handleSubmit.bind(this);
        this.handleUpdate = this.handleUpdate.bind(this);
    }

    handleArtistChange=(event) =>{
        this.setState({id: event.target.value});
    }

    handleNameChange=(event) =>{
        this.setState({name: event.target.value});
    }

    handleGenreChange=(event) =>{
        this.setState({genre: event.target.value});
    }
    handleSubmit =(event) =>{

        var artist={id:this.state.id,
            name:this.state.name,
            genre:this.state.genre
        }
        console.log('An artist was submitted: ');
        console.log(artist);
        this.props.addFunc(artist);

        event.preventDefault();
    }

    handleUpdate =() =>{

        var artist={id:this.state.id,
            name:this.state.name,
            genre:this.state.genre
        }
        console.log('An artist was submitted: ');
        console.log(artist);
        this.props.updateFunc(artist);
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <label>
                    ID:
                    <input type="text" value={this.state.id} onChange={this.handleArtistChange} />
                </label><br/>
                <label>
                    Name:
                    <input type="text" value={this.state.name} onChange={this.handleNameChange} />
                </label><br/>
                <label>
                    Genre:
                    <input type="text" value={this.state.genre} onChange={this.handleGenreChange} />
                </label><br/>

                <input type="submit" value="Add"  />
                <button type="button" onClick={this.handleUpdate}>Update</button>
            </form>
        );
    }
}
export default ArtistForm;