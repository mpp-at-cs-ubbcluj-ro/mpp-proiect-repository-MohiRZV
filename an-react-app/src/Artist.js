import React from  'react';
import './ArtistApp.css'

class ArtistRow extends React.Component{

    handleClick=(event)=>{
        console.log('delete button for '+this.props.artist.id);
        this.props.deleteFunc(this.props.artist.id);
    }

    render() {
        return (
            <tr>
                <td>{this.props.artist.id}</td>
                <td>{this.props.artist.name}</td>
                <td>{this.props.artist.genre}</td>
                <td><button  onClick={this.handleClick}>Delete</button></td>
            </tr>
        );
    }
}
/*<form onSubmit={this.handleClicke}><input type="submit" value="Delete"/></form>*/
/**/
class ArtistTable extends React.Component {
    render() {
        var rows = [];
        var functieStergere=this.props.deleteFunc;
        this.props.artists.forEach(function(a){rows.push(<ArtistRow artist={a} key={a.id} deleteFunc={functieStergere} />)})
        // for (var a of Object.entries(this.props.artists)){
        //     rows.push(<ArtistRow artist={a} key={a.id} deleteFunc={functieStergere} />);
        // }
        return (<div className="ArtistTable">

                <table className="center">
                    <thead>
                    <tr>
                        <th>ID</th>
                        <th>Name</th>
                        <th>Genre</th>
                        <th>Btn</th>
                    </tr>
                    </thead>
                    <tbody>{rows}</tbody>
                </table>

            </div>
        );
    }
}

export default ArtistTable;