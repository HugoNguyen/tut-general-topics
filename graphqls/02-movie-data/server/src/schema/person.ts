import { Field, ID, Int, InterfaceType, ObjectType } from "type-graphql";

@InterfaceType()
export abstract class IPerson {
    @Field(() => ID)
    id: number;
    @Field()
    name: string;
}

@ObjectType({ description: "The Crew type", implements: IPerson })
export class Crew extends IPerson {
    @Field()
    department: string;
    @Field()
    job: string;
}

@ObjectType({ description: 'The Cast type', implements: IPerson })
export class Cast extends IPerson {
    @Field(() => Int)
    cast_id: number;
    @Field()
    character:string;
    @Field(() => Int)
    order: number;
}