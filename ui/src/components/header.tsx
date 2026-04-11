

export const Header = ({headerText, description}: {headerText: string, description?: string}) => {

    return (
        <div className="pb-2">
            <h1 className="text-[3rem] font-heading leading-none pb-2 font-semibold text-card-foreground">{headerText}</h1>
            {description && <p className="text-sm leading-none -translate-y-2 text-muted-foreground">{description}</p>}
        </div>
    )
}