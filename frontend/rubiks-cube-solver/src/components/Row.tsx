export interface IRowProps {
    nodes: string[];
    index: string;
}

export const Row = ({ nodes, index }: IRowProps) => {
    return <div className="row">
        {nodes.map((x, i) => {
            return (
                <div key={`${index}_${i}`} className={`node ${x.toLocaleLowerCase()}`}></div>
            )
        })}
    </div>
}