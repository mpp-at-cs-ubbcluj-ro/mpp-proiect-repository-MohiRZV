
import React from  'react';
import ArtistTable from './Artist';
import './ArtistApp.css'
import ArtistForm from "./ArtistForm";
import {GetArtists, DeleteArtist, AddArtist, UpdateArtist} from './utils/rest-calls'


class ArtistApp extends React.Component{
    constructor(props){
        super(props);
        this.state={artists:[{"id":"404","name":"John Doe","genre":"miss"}],
            deleteFunc:this.deleteFunc.bind(this),
            addFunc:this.addFunc.bind(this),
            updateFunc:this.updateFunc.bind(this)
        }
        console.log('ArtistApp constructor')
    }

    addFunc(artist){
        console.log('inside add Func '+artist);
        AddArtist(artist)
            .then(res=> GetArtists())
            .then(artists=>this.setState({artists}))
            .catch(erorr=>console.log('eroare add ',erorr));
    }

    updateFunc(artist){
        console.log('inside update Func '+artist);
        UpdateArtist(artist)
            .then(res=> GetArtists())
            .then(artists=>this.setState({artists}))
            .catch(erorr=>console.log('eroare update ',erorr));
    }


    deleteFunc(artist){
        console.log('inside deleteFunc '+artist);
        DeleteArtist(artist)
            .then(res=> GetArtists())
            .then(artists=>this.setState({artists}))
            .catch(error=>console.log('eroare delete', error));
    }


    componentDidMount(){
        console.log('inside componentDidMount')
        GetArtists().then(artists=>this.setState({artists}));
    }

    render(){
        return(
            <div className="ArtistApp">
                <h1>Artist Management</h1>
                <ArtistForm addFunc={this.state.addFunc} updateFunc={this.state.updateFunc}/>
                <br/>
                <br/>
                <ArtistTable artists={this.state.artists} deleteFunc={this.state.deleteFunc}/>
            </div>
        );
    }
}

export default ArtistApp;