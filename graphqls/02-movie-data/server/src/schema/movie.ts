import { Field, ID, ObjectType } from "type-graphql";
import { Cast, Crew } from "./person";

@ObjectType({ description: 'The Movie model '})
export class Movie {
    @Field(() => ID)
    id: number;
    @Field()
    imdb_id: string;
    @Field()
    title: string;
    @Field()
    overview: string;

    @Field(() => Crew)
    director: Crew;
    @Field(() => [Cast])
    casts: Cast[];
}
