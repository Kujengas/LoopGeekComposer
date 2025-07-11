import { useEffect, useState } from 'react';
import './App.css';

interface Chord {
    name: string;
    notes:[]
}

function App() {
    const [chords, setChordProgression] = useState<Chord[]>();

    useEffect(() => {
        generateChordProgressionData();
    }, []);

    const contents = chords === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        :<>
            <ul>
                {chords.map(chord =>
                    <li key={chord.name}>
                    </li>
                )}
            </ul></>;

    return (
        <div>
            <h1>Chord Progression</h1>
            {contents}
        </div>
    );

    async function generateChordProgressionData() {
        const response = await fetch('chordprogression');
        if (response.ok) {
            const data = await response.json();
            setChordProgression(data);
        }
    }

}

export default App;