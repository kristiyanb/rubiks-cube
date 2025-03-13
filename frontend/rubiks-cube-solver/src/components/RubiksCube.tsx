import { useEffect, useState } from "react";
import { ICube } from "../models/cube";
import { Side } from "./Side";
import { environment } from "../environment";

export const RubiksCube = () => {
    const [cube, setCube] = useState<ICube | undefined>();
    const [loading, setLoading] = useState(true);
    const [x, setX] = useState(-20);
    const [y, setY] = useState(-45);

    const getCube = async () => {
        const result = await fetch(environment.apiUrl);
        const cubeState = await result.json();

        setCube(cubeState);
        setLoading(false);
    };

    useEffect(() => {
        getCube();
    }, []);

    const rotateCube = async (side: string, clockwise: boolean) => {
        setLoading(true);

        const result = await fetch(environment.apiUrl, {
            method: 'POST',
            body: JSON.stringify({ side, clockwise }),
            headers: { "Content-Type": "application/json" }
        });
        const updatedCubeState = await result.json();

        setCube(updatedCubeState);
        setLoading(false);
    };

    const resetCube = async () => {
        setLoading(true);

        const result = await fetch(`${environment.apiUrl}reset`, { method: "DELETE" });
        const cubeState = await result.json();

        setCube(cubeState);
        setLoading(false);
    }

    return (
        <>
            {
                loading ?
                    <main><div>Loading...</div></main>
                    : (<>
                        <main>
                            <div className="container">
                                {cube && (<div className="cube" style={{
                                    "transform": `rotateX(${ x }deg) rotateY(${ y }deg)`

                                }}>
                                    <Side className="face front" side={cube?.front} />
                                    <Side className="face back" side={cube?.back} />
                                    <Side className="face up" side={cube?.up} />
                                    <Side className="face right" side={cube?.right} />
                                    <Side className="face left" side={cube?.left} />
                                    <Side className="face down" side={cube?.down} />
                                </div>
                                )}
                            </div>
                        </main>
                        <div className="controls">
                            <button className="control rotate-right" onClick={() => setY(y + 90)}>{">"}</button>
                            <button className="control" onClick={() => setY(y - 90)}>{"<"}</button>
                            <button className="control" onClick={() => setX(x + 50)}>{"^"}</button>
                            <button className="control" onClick={() => setX(x - 50)}>{"v"}</button>
                        </div>
                        <div className="buttons-container">
                            <div className="buttons">

                                <div className="btn" onClick={() => rotateCube('f', true)}>
                                    F
                                </div>
                                <div className="btn" onClick={() => rotateCube('r', true)}>
                                    R
                                </div>
                                <div className="btn" onClick={() => rotateCube('u', true)}>
                                    U
                                </div>
                                <div className="btn" onClick={() => rotateCube('b', true)}>
                                    B
                                </div>
                                <div className="btn" onClick={() => rotateCube('l', true)}>
                                    L
                                </div>
                                <div className="btn" onClick={() => rotateCube('d', true)}>
                                    D
                                </div>

                            </div>
                            <div className="buttons">
                                <div className="btn" onClick={() => rotateCube('f', false)}>
                                    F'
                                </div>
                                <div className="btn" onClick={() => rotateCube('r', false)}>
                                    R'
                                </div>
                                <div className="btn" onClick={() => rotateCube('u', false)}>
                                    U'
                                </div>
                                <div className="btn" onClick={() => rotateCube('b', false)}>
                                    B'
                                </div>
                                <div className="btn" onClick={() => rotateCube('l', false)}>
                                    L'
                                </div>
                                <div className="btn" onClick={() => rotateCube('d', false)}>
                                    D'
                                </div>
                            </div>
                        </div>
                        <div className="reset">
                            <div className="reset-btn" onClick={() => resetCube()}>Reset</div>
                        </div>
                        <div className="expanded-view">
                            {cube && (<div className="expanded-cube">
                                <div className="expanded-cube-row offset-left">
                                    <Side className="flat" side={cube?.up} />
                                </div>
                                <div className="expanded-cube-row">
                                    <Side className="flat" side={cube?.left} />
                                    <Side className="flat" side={cube?.front} />
                                    <Side className="flat" side={cube?.right} />
                                    <Side className="flat" side={cube?.back} />
                                </div>
                                <div className="expanded-cube-row offset-left">
                                    <Side className="flat" side={cube?.down} />
                                </div>
                            </div>
                            )}
                        </div>
                    </>)
            }
        </>
    )
}