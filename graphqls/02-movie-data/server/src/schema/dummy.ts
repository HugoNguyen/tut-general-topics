import { Field, ID, ObjectType } from "type-graphql";
import { DummyStatus } from "./dummy-status-enum";

@ObjectType({ description: 'The Dummy Type'})
export class Dummy {
    @Field(() => ID)
    id: number;
    @Field()
    name: string;
    @Field()
    updatedAt: Date;
    @Field(() => DummyStatus)
    status: DummyStatus;
}
